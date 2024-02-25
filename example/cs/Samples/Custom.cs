
using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Derive;

using AUTD3Sharp.Utils;

namespace Samples;

[Gain]
partial class MyFocus
{
    private readonly Vector3d _point;

    public MyFocus(Vector3d point)
    {
        _point = point;
    }

    private Dictionary<int, Drive[]> Calc(Geometry geometry)
    {
        return Transform(geometry, (dev, tr) =>
        {
            var tp = tr.Position;
            var dist = (tp - _point).L2Norm;
            var phase = Phase.FromRad(dist * tr.Wavenumber(dev.SoundSpeed));
            return new Drive { Phase = phase, Intensity = EmitIntensity.Max };
        });
    }
}

internal static class CustomTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = ConfigureSilencer.Default();
        await autd.SendAsync(config);

        var m = new Sine(150);
        var g = new MyFocus(autd.Geometry.Center + new Vector3d(0, 0, 150));

        await autd.SendAsync((m, g));
    }
}
