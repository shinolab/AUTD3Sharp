using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public class STMSamplingConfig
    {
        internal STMSamplingConfigWrap Inner;

        private STMSamplingConfig(STMSamplingConfigWrap inner)
        {
            Inner = inner;
        }

        public static STMSamplingConfig Freq(Freq<float> f) => new (NativeMethodsBase.AUTDSTMSamplingConfigFromFreq(f.Hz));

        public static STMSamplingConfig FreqNearest(Freq<float> f) =>
          new (NativeMethodsBase.AUTDSTMSamplingConfigFromFreqNearest(f.Hz));

        public static STMSamplingConfig SamplingConfig(SamplingConfig c) => new (NativeMethodsBase.AUTDSTMSamplingConfigFromSamplingConfig(c.Inner));
    }
}
