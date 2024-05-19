using AUTD3Sharp;
using AUTD3Sharp.Gain.Holo;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class GainHoloTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = Silencer.Default();
        await autd.SendAsync(config);

        var center = autd.Geometry.Center + new Vector3d(0, 0, 150);

        var backend = new NalgebraBackend();
        var g = new GSPAT<NalgebraBackend>(backend)
            .AddFocus(center + 20.0 * Vector3d.UnitX, 5e3 * Pa)
            .AddFocus(center - 20.0 * Vector3d.UnitX, 5e3 * Pa);

        var m = new Sine(150 * Hz);

        await autd.SendAsync(m, g);
    }
}
