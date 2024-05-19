using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class PlaneWaveTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = Silencer.Default();
        await autd.SendAsync(config);

        var m = new Sine(150 * Hz); // AM sin 150 Hz

        var dir = Vector3d.UnitZ;
        var g = new Plane(dir);

        await autd.SendAsync((m, g));
    }
}
