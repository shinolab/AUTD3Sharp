using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class PlaneWaveTest
{
    public static void Test(Controller autd)
    {
        var config = new Silencer();
        autd.Send(config);

        var m = new Sine(freq: 150 * Hz, option: new SineOption());

        var dir = Vector3.UnitZ;
        var g = new Plane(dir: dir, option: new PlaneOption());

        autd.Send((m, g));
    }
}
