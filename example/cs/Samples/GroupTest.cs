﻿using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;

namespace Samples;

internal static class GroupByDeviceTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = ConfigureSilencer.Default();
        await autd.SendAsync(config);

        await autd.Group(dev =>
            {
                return dev.Idx switch
                {
                    0 => "null",
                    1 => "focus",
                    _ => null
                };
            })
            .Set("null", (new Static(), new Null()))
            .Set("focus", (new Sine(150), new Focus(autd.Geometry.Center + new Vector3d(0, 0, 150))))
            .SendAsync();
    }
}


internal static class GroupByTransducerTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = ConfigureSilencer.Default();
        await autd.SendAsync(config);

        var cx = autd.Geometry.Center.x;
        var g1 = new Focus(autd.Geometry.Center + new Vector3d(0, 0, 150));
        var g2 = new Null();

        var g = new Group(
            (_, tr) => tr.Position.x < cx ? "focus" : "null"
            ).Set("focus", g1).Set("null", g2);

        var m = new Sine(150);

        await autd.SendAsync(m, g);
    }
}
