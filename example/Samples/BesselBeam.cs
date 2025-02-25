using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class BesselBeamTest
{
    public static void Test(Controller autd)
    {
        var config = new Silencer();
        autd.Send(config);

        var m = new Sine(freq: 150 * Hz, option: new SineOption());

        var start = autd.Center();
        var dir = Vector3.UnitZ;
        var g = new Bessel(start, dir, 13.0f / 180.0f * MathF.PI * rad, new BesselOption());

        autd.Send((m, g));
    }
}
