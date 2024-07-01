using AUTD3Sharp;
using AUTD3Sharp.Gain.Holo;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class GainHoloTest
{
    public static void Test<T>(Controller<T> autd)
    {
        var config = Silencer.Default();
        autd.Send(config);

        var center = autd.Geometry.Center + new Vector3(0, 0, 150);

        var backend = new NalgebraBackend();
        var g = new GSPAT(backend, [(center + 20.0f * Vector3.UnitX, 5e3f * Pa), (center - 20.0f * Vector3.UnitX, 5e3f * Pa)]);

        var m = new Sine(150 * Hz);

        autd.Send((m, g));
    }
}
