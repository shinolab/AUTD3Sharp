using AUTD3Sharp.NativeMethods;
using System.Runtime.InteropServices;

namespace AUTD3Sharp
{
    internal enum STMSamplingConfigTag : uint
    {
        Freq,
        Period,
        Config,
        FreqNearest,
        PeriodNearest
    }

    [StructLayout(LayoutKind.Explicit)]
    public class STMSamplingConfig
    {
        [FieldOffset(0)]
        internal readonly STMSamplingConfigTag _tag;

        [FieldOffset(8)]
        private readonly Freq<float> _f;

        [FieldOffset(8)]
        private readonly Duration _period;

        [FieldOffset(8)]
        private readonly NativeMethods.SamplingConfigWrap _config;

        internal STMSamplingConfig IntoNearest()
        {
            return _tag switch
            {
                STMSamplingConfigTag.Freq => new STMSamplingConfig(STMSamplingConfigTag.FreqNearest, _f),
                STMSamplingConfigTag.FreqNearest => new STMSamplingConfig(STMSamplingConfigTag.FreqNearest, _f),
                STMSamplingConfigTag.Period => new STMSamplingConfig(STMSamplingConfigTag.PeriodNearest, _period),
                STMSamplingConfigTag.PeriodNearest => new STMSamplingConfig(STMSamplingConfigTag.PeriodNearest, _period),
                _ => this
            };
        }

        private STMSamplingConfig(STMSamplingConfigTag tag, Freq<float> f)
        {
            _tag = tag;
            _f = f;
        }
        private STMSamplingConfig(STMSamplingConfigTag tag, Duration period)
        {
            _tag = tag;
            _period = period;
        }
        private STMSamplingConfig(Freq<float> f)
        {
            _tag = STMSamplingConfigTag.Freq;
            _f = f;
        }
        private STMSamplingConfig(Duration period)
        {
            _tag = STMSamplingConfigTag.Period;
            _period = period;
        }
        private STMSamplingConfig(SamplingConfig config)
        {
            _tag = STMSamplingConfigTag.Config;
            _config = config.Inner;
        }

        public static implicit operator STMSamplingConfig(Freq<float> f) => new(f);
        public static implicit operator STMSamplingConfig(Duration period) => new(period);
        public static implicit operator STMSamplingConfig(SamplingConfig config) => new(config);

        internal SamplingConfig SamplingConfig(int n) => _tag switch
        {
            STMSamplingConfigTag.Freq => new SamplingConfig(NativeMethodsBase.AUTDSTMConfigFromFreq(_f.Hz, (ushort)n).Validate()),
            STMSamplingConfigTag.Period => new SamplingConfig(NativeMethodsBase.AUTDSTMConfigFromPeriod(_period, (ushort)n).Validate()),
            STMSamplingConfigTag.FreqNearest => new SamplingConfig(NativeMethodsBase.AUTDSTMConfigFromFreqNearest(_f.Hz, (ushort)n)),
            STMSamplingConfigTag.PeriodNearest => new SamplingConfig(NativeMethodsBase.AUTDSTMConfigFromPeriodNearest(_period, (ushort)n)),
            _ => new SamplingConfig(_config)
        };
    }
}
