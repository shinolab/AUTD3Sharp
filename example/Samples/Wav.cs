using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation.AudioFile;
using AUTD3Sharp.Utils;

namespace Samples;

internal static class WavTest
{
    public static void Test<T>(Controller<T> autd)
    {
        var config = new Silencer();
        autd.Send(config);

        var m = new Wav("sin150.wav");
        var g = new Focus(autd.Center() + new Vector3(0, 0, 150));
        autd.Send((m, g));
    }
}
