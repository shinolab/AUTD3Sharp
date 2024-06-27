using System;
using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp
{
    public class SamplingConfig
    {
        internal SamplingConfigWrap Inner;

        public SamplingConfig(SamplingConfigWrap inner)
        {
            Inner = inner;
        }

        public static explicit operator SamplingConfigWrap(SamplingConfig config) => config.Inner;
        public static implicit operator SamplingConfig(Freq<uint> f) => new(NativeMethodsBase.AUTDSamplingConfigFromFreq(f.Hz));
        public static implicit operator SamplingConfig(TimeSpan period) => new(NativeMethodsBase.AUTDSamplingConfigFromPeriod((ulong)(period.TotalMilliseconds * 1000 * 1000)));

        public static SamplingConfig FreqNearest(Freq<float> f) =>
            new(NativeMethodsBase.AUTDSamplingConfigFromFreqNearest(f.Hz));

        public static SamplingConfig PeriodNearest(TimeSpan period) =>
            new(NativeMethodsBase.AUTDSamplingConfigFromPeriodNearest((ulong)(period.TotalMilliseconds * 1000 * 1000)));

        public static SamplingConfig Division(uint d) => new(
            NativeMethodsBase.AUTDSamplingConfigFromDivision(d));

        public static SamplingConfig DivisionRaw(uint d) =>
            new(NativeMethodsBase.AUTDSamplingConfigFromDivisionRaw(d));

        public Freq<float> Freq => NativeMethodsBase.AUTDSamplingConfigFreq(Inner).Validate() * Hz;
        public TimeSpan Period => TimeSpan.FromMilliseconds((double)NativeMethodsBase.AUTDSamplingConfigPeriod(Inner).Validate() / 1000 / 1000);
        public uint Div => NativeMethodsBase.AUTDSamplingConfigDivision(Inner).Validate();
    }
}
