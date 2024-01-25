using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;

namespace Samples;

internal static class PlaneWaveTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = ConfigureSilencer.Default();
        await autd.SendAsync(config);

        var m = new Sine(150); // AM sin 150 Hz

        var dir = Vector3d.UnitZ;
        var g = new Plane(dir);

        await autd.SendAsync((m, g));
    }
}
