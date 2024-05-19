using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation.AudioFile;
using AUTD3Sharp.Utils;

namespace Samples;

internal static class WavTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = Silencer.Default();
        await autd.SendAsync(config);

        var m = new Wav("sin150.wav");
        var g = new Focus(autd.Geometry.Center + new Vector3d(0, 0, 150));
        await autd.SendAsync((m, g));
    }
}
