using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class GainSTMTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = Silencer.Disable();
        await autd.SendAsync(config);

        var m = new Static();

        var center = autd.Geometry.Center + new Vector3d(0, 0, 150);
        const int pointNum = 50;
        const double radius = 30.0;
        var stm = GainSTM.FromFreq(1.0 * Hz).AddGainsFromIter(Enumerable.Range(0, pointNum).Select(i =>
        {
            var theta = 2.0 * Math.PI * i / pointNum;
            return new Focus(center + radius * new Vector3d(Math.Cos(theta), Math.Sin(theta), 0));
        }));

        await autd.SendAsync((m, stm));
    }
}

internal static class FocusSTMTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        var config = Silencer.Disable();
        await autd.SendAsync(config);

        var mod = new Static();
        await autd.SendAsync(mod);

        var center = autd.Geometry.Center + new Vector3d(0, 0, 150);
        const int pointNum = 200;
        const double radius = 30.0;
        var stm = FocusSTM.FromFreq(1.0 * Hz).AddFociFromIter(Enumerable.Range(0, pointNum).Select(i =>
        {
            var theta = 2.0 * Math.PI * i / pointNum;
            return center + radius * new Vector3d(Math.Cos(theta), Math.Sin(theta), 0);
        }));

        await autd.SendAsync(stm);
    }
}
