using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class FocusTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = Silencer.Default();
        await autd.SendAsync(config);

        var m = new Sine(150 * Hz);
        var g = new Focus(autd.Geometry.Center + new Vector3d(0, 0, 150));
        await autd.SendAsync((m, g));
    }
}
