﻿using AUTD3Sharp;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

[Gain]
partial class MyFocus(Vector3 point)
{
    private Func<Device, Func<Transducer, Drive>> Calc(Geometry _geometry)
    {
        return Transform(dev => tr =>
        {
            var tp = tr.Position;
            var dist = (tp - point).L2Norm;
            var phase = new Phase(dist * dev.Wavenumber * rad);
            return new Drive { Phase = phase, Intensity = EmitIntensity.Max };
        });
    }
}

internal static class UserDefinedTest
{
    public static void Test<T>(Controller<T> autd)
    {
        var config = new Silencer();
        autd.Send(config);

        var m = new Sine(150 * Hz);
        var g = new MyFocus(autd.Geometry.Center + new Vector3(0, 0, 150));

        autd.Send((m, g));
    }
}
