using System;
using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp
{
    public class STMSamplingConfig
    {
        internal NativeMethods.SamplingConfig Inner;
        internal int N;

        private STMSamplingConfig(NativeMethods.SamplingConfig inner, int n)
        {
            Inner = inner;
            N = n;
        }

        internal STMSamplingConfig(Freq<float> f, int n) : this(NativeMethodsBase.AUTDSTMConfigFromFreq(f.Hz, (ushort)n).Validate(), n) { }
        internal STMSamplingConfig(TimeSpan period, int n) : this(NativeMethodsBase.AUTDSTMConfigFromPeriod((ulong)(period.TotalMilliseconds * 1000 * 1000), (ushort)n).Validate(), n) { }
        internal STMSamplingConfig(SamplingConfig config, int n) : this(config.Inner, n) { }

        public static STMSamplingConfig Nearest(Freq<float> f, int n) => new(NativeMethodsBase.AUTDSTMConfigFromFreqNearest(f.Hz, (ushort)n).Validate(), n);
        public static STMSamplingConfig Nearest(TimeSpan period, int n) => new(NativeMethodsBase.AUTDSTMConfigFromPeriodNearest((ulong)(period.TotalMilliseconds * 1000 * 1000), (ushort)n).Validate(), n);

        public Freq<float> Freq => NativeMethodsBase.AUTDSTMFreq(Inner, (uint)N) * Hz;
        public TimeSpan Period => TimeSpan.FromMilliseconds((double)NativeMethodsBase.AUTDSTMPeriod(Inner, (uint)N) / 1000 / 1000);
        public SamplingConfig SamplingConfig => new(Inner);
    }
}
