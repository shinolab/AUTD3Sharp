using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public static class SamplingConfig
    {
        public static SamplingConfigWrap Freq(uint f) => NativeMethodsBase.AUTDSamplingConfigFromFreq(f);

        public static SamplingConfigWrap FreqNearest(double f) =>
            NativeMethodsBase.AUTDSamplingConfigFromFreqNearest(f);

        public static SamplingConfigWrap Division(uint d) =>
            NativeMethodsBase.AUTDSamplingConfigFromDivision(d);

        public static SamplingConfigWrap DivisionRaw(uint d) =>
            NativeMethodsBase.AUTDSamplingConfigFromDivisionRaw(d);

    }
}
