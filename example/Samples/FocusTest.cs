using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class FocusTest
{
    public static void Test<T>(Controller<T> autd)
    {
        var config = new Silencer();
        autd.Send(config);

        var m = new Sine(150 * Hz);
        var g = new Focus(autd.Center + new Vector3(0, 0, 150));
        autd.Send((m, g));
    }
}
