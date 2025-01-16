using AUTD3Sharp.NativeMethods;
using System.Runtime.InteropServices;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp
{
    internal enum STMSamplingConfigTag : uint
    {
        Freq,
#if !DYNAMIC_FREQ
        Period,
#endif
        Config
    }

    [StructLayout(LayoutKind.Explicit)]
    public class STMSamplingConfig
    {
        [FieldOffset(0)]
        private readonly STMSamplingConfigTag _tag;

        [FieldOffset(8)]
        private readonly Freq<float> _f;

        [FieldOffset(8)]
        private readonly Duration _period;

        [FieldOffset(8)]
        private readonly SamplingConfig _config;

        private STMSamplingConfig(Freq<float> f)
        {
            _tag = STMSamplingConfigTag.Freq;
            _f = f;
        }

#if !DYNAMIC_FREQ
        private STMSamplingConfig(Duration period)
        {
            _tag = STMSamplingConfigTag.Period;
            _period = period;
        }
#endif

        private STMSamplingConfig(SamplingConfig config)
        {
            _tag = STMSamplingConfigTag.Config;
            _config = config;
        }

        public static implicit operator STMSamplingConfig(Freq<float> f) => new(f);
        public static implicit operator STMSamplingConfig(Duration period) => new(period);
        public static implicit operator STMSamplingConfig(SamplingConfig config) => new(config);

        internal SamplingConfig SamplingConfig(int n) => _tag switch
        {
            STMSamplingConfigTag.Freq => NativeMethodsBase.AUTDSTMConfigFromFreq(_f.Hz, (ushort)n).Validate(),
#if !DYNAMIC_FREQ
            STMSamplingConfigTag.Period => NativeMethodsBase.AUTDSTMConfigFromPeriod(_period, (ushort)n).Validate(),
#endif
            _ => _config
        };

        public Freq<float> Freq(int n) => NativeMethodsBase.AUTDSTMFreq(SamplingConfig(n), (ushort)n) * Hz;
#if !DYNAMIC_FREQ
        public Duration Period(int n) => NativeMethodsBase.AUTDSTMPeriod(SamplingConfig(n), (ushort)n);
#endif
    }

    [StructLayout(LayoutKind.Explicit)]
    public class STMSamplingConfigNearest
    {
        [FieldOffset(0)]
        private readonly STMSamplingConfigTag _tag;

        [FieldOffset(8)]
        private readonly Freq<float> _f;

        [FieldOffset(8)]
        private readonly Duration _period;

        private STMSamplingConfigNearest(Freq<float> f)
        {
            _tag = STMSamplingConfigTag.Freq;
            _f = f;
        }

#if !DYNAMIC_FREQ
        private STMSamplingConfigNearest(Duration period)
        {
            _tag = STMSamplingConfigTag.Period;
            _period = period;
        }
#endif

        public static implicit operator STMSamplingConfigNearest(Freq<float> f) => new(f);
#if !DYNAMIC_FREQ
        public static implicit operator STMSamplingConfigNearest(Duration period) => new(period);
#endif

        internal STMSamplingConfig STMSamplingConfig(int n) => _tag switch
        {
            STMSamplingConfigTag.Freq => new SamplingConfig(NativeMethodsBase.AUTDSTMConfigFromFreqNearest(_f.Hz, (ushort)n).Validate()),
#if DYNAMIC_FREQ
            _ => throw new NotImplementedException(),
#else
            _ => new SamplingConfig(NativeMethodsBase.AUTDSTMConfigFromPeriodNearest(_period, (ushort)n).Validate())
#endif
        };
    }
}
