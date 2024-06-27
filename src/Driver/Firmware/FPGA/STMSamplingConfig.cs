using System;
using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp
{
    public class STMSamplingConfig
    {
        internal STMSamplingConfigWrap Inner;

        private STMSamplingConfig(STMSamplingConfigWrap inner)
        {
            Inner = inner;
        }

        public static STMSamplingConfig FromFreq(Freq<float> f) => new(NativeMethodsBase.AUTDSTMSamplingConfigFromFreq(f.Hz));

        public static STMSamplingConfig FromFreqNearest(Freq<float> f) =>
          new(NativeMethodsBase.AUTDSTMSamplingConfigFromFreqNearest(f.Hz));

        public static STMSamplingConfig FromPeriod(TimeSpan period) =>
            new(NativeMethodsBase.AUTDSTMSamplingConfigFromPeriod((ulong)(period.TotalMilliseconds * 1000 * 1000)));

        public static STMSamplingConfig FromPeriodNearest(TimeSpan period) =>
            new(NativeMethodsBase.AUTDSTMSamplingConfigFromPeriodNearest((ulong)(period.TotalMilliseconds * 1000 * 1000)));

        public static STMSamplingConfig FromSamplingConfig(SamplingConfig c) => new(NativeMethodsBase.AUTDSTMSamplingConfigFromSamplingConfig(c.Inner));

        public Freq<float> Freq(int n) => NativeMethodsBase.AUTDSTMFreq(Inner, (uint)n).Validate() * Hz;
        public TimeSpan Period(int n) => TimeSpan.FromMilliseconds((double)NativeMethodsBase.AUTDSTMPeriod(Inner, (uint)n).Validate() / 1000 / 1000);
        public SamplingConfig SamplingConfig(int n) => new(NativeMethodsBase.AUTDSTMSamplingSamplingConfig(Inner, (uint)n).Validate());
    }
}
