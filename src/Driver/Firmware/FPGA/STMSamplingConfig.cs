using System;
using AUTD3Sharp.NativeMethods;
using System.Runtime.InteropServices;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp
{
    internal enum STMSamplingConfigTag : uint
    {
        Freq,
        Period,
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
        private readonly TimeSpan _period;

        [FieldOffset(8)]
        private readonly NativeMethods.SamplingConfig _config;

        private STMSamplingConfig(Freq<float> f)
        {
            _tag = STMSamplingConfigTag.Freq;
            _f = f;
        }

        private STMSamplingConfig(TimeSpan period)
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
        public static implicit operator STMSamplingConfig(TimeSpan period) => new(period);
        public static implicit operator STMSamplingConfig(SamplingConfig config) => new(config);

        internal NativeMethods.SamplingConfig SamplingConfig(int n) => _tag switch
        {
            STMSamplingConfigTag.Freq => NativeMethodsBase.AUTDSTMConfigFromFreq(_f.Hz, (ushort)n).Validate(),
            STMSamplingConfigTag.Period => NativeMethodsBase.AUTDSTMConfigFromPeriod((ulong)(_period.TotalMilliseconds * 1000 * 1000), (ushort)n).Validate(),
            STMSamplingConfigTag.Config => _config,
            _ => throw new ArgumentOutOfRangeException()
        };

        public Freq<float> Freq(int n) => NativeMethodsBase.AUTDSTMFreq(SamplingConfig(n), (uint)n) * Hz;
        public TimeSpan Period(int n) => TimeSpan.FromMilliseconds((double)NativeMethodsBase.AUTDSTMPeriod(SamplingConfig(n), (uint)n) / 1000 / 1000);
    }

    [StructLayout(LayoutKind.Explicit)]
    public class STMSamplingConfigNearest
    {
        [FieldOffset(0)]
        private readonly STMSamplingConfigTag _tag;

        [FieldOffset(8)]
        private readonly Freq<float> _f;

        [FieldOffset(8)]
        private readonly TimeSpan _period;

        private STMSamplingConfigNearest(Freq<float> f)
        {
            _tag = STMSamplingConfigTag.Freq;
            _f = f;
        }

        private STMSamplingConfigNearest(TimeSpan period)
        {
            _tag = STMSamplingConfigTag.Period;
            _period = period;
        }

        public static implicit operator STMSamplingConfigNearest(Freq<float> f) => new(f);
        public static implicit operator STMSamplingConfigNearest(TimeSpan period) => new(period);

        internal STMSamplingConfig STMSamplingConfig(int n) => _tag switch
        {
            STMSamplingConfigTag.Freq => new SamplingConfig(NativeMethodsBase.AUTDSTMConfigFromFreqNearest(_f.Hz, (ushort)n).Validate()),
            STMSamplingConfigTag.Period => new SamplingConfig(NativeMethodsBase.AUTDSTMConfigFromPeriodNearest((ulong)(_period.TotalMilliseconds * 1000 * 1000), (ushort)n).Validate()),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
