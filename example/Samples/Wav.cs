using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation.AudioFile;
using AUTD3Sharp.Utils;

namespace Samples;

internal static class WavTest
{
    public static void Test(Controller autd)
    {
        var config = new Silencer();
        autd.Send(config);

        var m = new Wav(path: "sin150.wav");
        var g = new Focus(pos: autd.Center() + new Vector3(0, 0, 150), option: new FocusOption());
        autd.Send((m, g));
    }
}
