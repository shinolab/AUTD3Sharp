using System;
using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp
{
    public class SamplingConfig
    {
        internal NativeMethods.SamplingConfig Inner;

        public SamplingConfig(NativeMethods.SamplingConfig inner)
        {
            Inner = inner;
        }

        public SamplingConfig(ushort div)
        {
            Inner = NativeMethodsBase.AUTDSamplingConfigFromDivision(div).Validate();
        }

        public SamplingConfig(Freq<uint> f) : this(NativeMethodsBase.AUTDSamplingConfigFromFreq(f.Hz).Validate()) { }
        public SamplingConfig(Freq<float> f) : this(NativeMethodsBase.AUTDSamplingConfigFromFreqF(f.Hz).Validate()) { }
        public SamplingConfig(TimeSpan period) : this(NativeMethodsBase.AUTDSamplingConfigFromPeriod((ulong)(period.TotalMilliseconds * 1000 * 1000)).Validate()) { }

        public static explicit operator NativeMethods.SamplingConfig(SamplingConfig config) => config.Inner;
        public static implicit operator SamplingConfig(Freq<uint> f) => new(f);
        public static implicit operator SamplingConfig(Freq<float> f) => new(f);
        public static implicit operator SamplingConfig(TimeSpan period) => new(period);

        public static SamplingConfig Nearest(Freq<float> f) => new(NativeMethodsBase.AUTDSamplingConfigFromFreqNearest(f.Hz));
        public static SamplingConfig Nearest(TimeSpan period) => new(NativeMethodsBase.AUTDSamplingConfigFromPeriodNearest((ulong)(period.TotalMilliseconds * 1000 * 1000)));

        public Freq<float> Freq => NativeMethodsBase.AUTDSamplingConfigFreq(Inner) * Hz;
        public TimeSpan Period => TimeSpan.FromMilliseconds((double)NativeMethodsBase.AUTDSamplingConfigPeriod(Inner) / 1000 / 1000);
        public ushort Division => NativeMethodsBase.AUTDSamplingConfigDivision(Inner);
    }
}
