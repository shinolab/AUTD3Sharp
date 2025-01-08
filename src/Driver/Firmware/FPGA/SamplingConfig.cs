using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SamplingConfig
    {
        public ushort Division { get; init; }

        internal SamplingConfig(SamplingConfig config)
        {
            Division = config.Division;
        }

        public SamplingConfig(ushort div) : this(NativeMethodsBase.AUTDSamplingConfigFromDivision(div).Validate())
        {
        }

        public SamplingConfig(Freq<uint> f) : this(NativeMethodsBase.AUTDSamplingConfigFromFreq(f.Hz).Validate()) { }
        public SamplingConfig(Freq<float> f) : this(NativeMethodsBase.AUTDSamplingConfigFromFreqF(f.Hz).Validate()) { }
#if !DYNAMIC_FREQ
        public SamplingConfig(Duration period) : this(NativeMethodsBase.AUTDSamplingConfigFromPeriod(period).Validate()) { }
#endif

        public static implicit operator SamplingConfig(Freq<uint> f) => new(f);
        public static implicit operator SamplingConfig(Freq<float> f) => new(f);
        public static implicit operator SamplingConfig(Duration period) => new(period);

        public static SamplingConfig Nearest(Freq<float> f) => new(NativeMethodsBase.AUTDSamplingConfigFromFreqNearest(f.Hz));
#if !DYNAMIC_FREQ
        public static SamplingConfig Nearest(Duration period) => new(NativeMethodsBase.AUTDSamplingConfigFromPeriodNearest(period));
#endif

        public Freq<float> Freq => NativeMethodsBase.AUTDSamplingConfigFreq(this) * Hz;

#if !DYNAMIC_FREQ
        public Duration Period => NativeMethodsBase.AUTDSamplingConfigPeriod(this);
#endif
    }
}
