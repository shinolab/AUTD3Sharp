using AUTD3Sharp;
using AUTD3Sharp.Modulation;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class CustomGain
{
    public static void Test(Controller autd)
    {
        var config = new Silencer();
        autd.Send(config);

        var m = new Sine(freq: 150 * Hz, option: new SineOption());
        var g = new AUTD3Sharp.Gain.Custom(
            (dev) => (tr) => (dev.Idx(), tr.Idx()) switch
            {
                (0, 0) => new Drive
                {
                    Phase = Phase.Zero,
                    Intensity = EmitIntensity.Max
                },
                (0, 248) => new Drive
                {
                    Phase = Phase.Zero,
                    Intensity = EmitIntensity.Max
                },
                _ => Drive.Null
            }
        );
        autd.Send((m, g));
    }
}
