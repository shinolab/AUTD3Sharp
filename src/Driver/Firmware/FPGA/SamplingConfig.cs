using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public static class SamplingConfig
    {
        public static SamplingConfigWrap Freq(Freq<uint> f) => NativeMethodsBase.AUTDSamplingConfigFromFreq(f.Hz);

        public static SamplingConfigWrap FreqNearest(Freq<double> f) =>
            NativeMethodsBase.AUTDSamplingConfigFromFreqNearest(f.Hz);

        public static SamplingConfigWrap Division(uint d) =>
            NativeMethodsBase.AUTDSamplingConfigFromDivision(d);

        public static SamplingConfigWrap DivisionRaw(uint d) =>
            NativeMethodsBase.AUTDSamplingConfigFromDivisionRaw(d);

    }
}
