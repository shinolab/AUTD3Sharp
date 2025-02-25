using AUTD3Sharp;
using AUTD3Sharp.Gain.Holo;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class GainHoloTest
{
    public static void Test(Controller autd)
    {
        var config = new Silencer();
        autd.Send(config);

        var center = autd.Center() + new Vector3(0, 0, 150);

        var backend = new NalgebraBackend();
        var g = new GSPAT(foci: [(center + 20.0f * Vector3.UnitX, 5e3f * Pa), (center - 20.0f * Vector3.UnitX, 5e3f * Pa)], option: new GSPATOption(), backend: backend);

        var m = new Sine(freq: 150 * Hz, option: new SineOption());

        autd.Send((m, g));
    }
}
