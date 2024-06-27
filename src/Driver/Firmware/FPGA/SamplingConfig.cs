using AUTD3Sharp.NativeMethods;

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
        public static explicit operator SamplingConfig(Freq<uint> f) => new(NativeMethodsBase.AUTDSamplingConfigFromFreq(f.Hz));

        public static SamplingConfig Freq(Freq<uint> f) => (SamplingConfig)f;

        public static SamplingConfig FreqNearest(Freq<float> f) =>
            new(NativeMethodsBase.AUTDSamplingConfigFromFreqNearest(f.Hz));

        public static SamplingConfig Division(uint d) => new(
            NativeMethodsBase.AUTDSamplingConfigFromDivision(d));

        public static SamplingConfig DivisionRaw(uint d) =>
            new(NativeMethodsBase.AUTDSamplingConfigFromDivisionRaw(d));

    }
}
