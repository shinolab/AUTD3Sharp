﻿using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class TransTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = Silencer.Default();
        await autd.SendAsync(config);

        var m = new Sine(150 * Hz);
        var g = new Custom(
            (dev, tr) => (dev.Idx, tr.Idx) switch
            {
                (0, 0) => new Drive(new Phase(0), EmitIntensity.Max),
                (0, 248) => new Drive(new Phase(0), EmitIntensity.Max),
                _ => Drive.Null
            }
        );
        await autd.SendAsync((m, g));
    }
}
