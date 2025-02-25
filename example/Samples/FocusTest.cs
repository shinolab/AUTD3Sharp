using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class FocusTest
{
    public static void Test(Controller autd)
    {
        var config = new Silencer();
        autd.Send(config);

        var m = new Sine(freq: 150 * Hz, option: new SineOption());
        var g = new Focus(pos: autd.Center() + new Vector3(0, 0, 150), option: new FocusOption());
        autd.Send((m, g));
    }
}
