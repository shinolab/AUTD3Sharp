using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class GroupByDeviceTest
{
    public static void Test<T>(Controller<T> autd)
    {
        var config = Silencer.Default();
        autd.Send(config);

        autd.Group(dev =>
           {
               return dev.Idx switch
               {
                   0 => "null",
                   1 => "focus",
                   _ => null
               };
           })
           .Set("null", (new Static(), new Null()))
           .Set("focus", (new Sine(150 * Hz), new Focus(autd.Geometry.Center + new Vector3d(0, 0, 150))))
           .Send();
    }
}


internal static class GroupByTransducerTest
{
    public static void Test<T>(Controller<T> autd)
    {
        var config = Silencer.Default();
        autd.Send(config);

        var cx = autd.Geometry.Center.X;
        var g1 = new Focus(autd.Geometry.Center + new Vector3d(0, 0, 150));
        var g2 = new Null();

        var g = new Group(
            (_, tr) => tr.Position.X < cx ? "focus" : "null"
            ).Set("focus", g1).Set("null", g2);

        var m = new Sine(150 * Hz);

        autd.Send(m, g);
    }
}
