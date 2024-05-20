using AUTD3Sharp;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

[Gain]
partial class MyFocus(Vector3d point)
{
    private Dictionary<int, Drive[]> Calc(Geometry geometry)
    {
        return Transform(geometry, (dev, tr) =>
        {
            var tp = tr.Position;
            var dist = (tp - point).L2Norm;
            var phase = Phase.FromRad(dist * dev.Wavenumber);
            return new Drive { Phase = phase, Intensity = EmitIntensity.Max };
        });
    }
}

internal static class UserDefinedTest
{
    public static void Test<T>(Controller<T> autd)
    {
        var config = Silencer.Default();
        autd.Send(config);

        var m = new Sine(150 * Hz);
        var g = new MyFocus(autd.Geometry.Center + new Vector3d(0, 0, 150));

        autd.Send((m, g));
    }
}
