using AUTD3Sharp;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class GainSTMTest
{
    public static void Test(Controller autd)
    {
        var config = Silencer.Disable();
        autd.Send(config);

        var m = new Static();

        var center = autd.Center() + new Vector3(0, 0, 150);
        const int pointNum = 50;
        const float radius = 30.0f;
        var stm = new GainSTM(gains: Enumerable.Range(0, pointNum).Select(i =>
        {
            var theta = 2.0f * MathF.PI * i / pointNum;
            return new Focus(pos: center + radius * new Vector3(MathF.Cos(theta), MathF.Sin(theta), 0), option: new FocusOption());
        }), config: 1.0f * Hz, option: new GainSTMOption());

        autd.Send((m, stm));
    }
}

internal static class FociSTMTest
{
    public static void Test(Controller autd)
    {
        var config = Silencer.Disable();
        autd.Send(config);

        var mod = new Static();
        autd.Send(mod);

        var center = autd.Center() + new Vector3(0, 0, 150);
        const int pointNum = 200;
        const float radius = 30.0f;
        var stm = new FociSTM(foci: Enumerable.Range(0, pointNum).Select(i =>
        {
            var theta = 2.0f * MathF.PI * i / pointNum;
            return center + radius * new Vector3(MathF.Cos(theta), MathF.Sin(theta), 0);
        }), config: 1.0f * Hz);

        autd.Send(stm);
    }
}
