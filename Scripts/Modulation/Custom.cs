using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Custom
    {
        private readonly byte[] _buf;
        private readonly (float, DynSincInterpolator)? _resample;

        public Custom(byte[] buf, SamplingConfig config)
        {
            _buf = buf;
            _config = config;
            _resample = null;
        }

        public Custom(byte[] buf, Freq<float> source, SamplingConfig target, SincInterpolation resampler)
        {
            _buf = buf;
            _config = target;
            _resample = (source.Hz, resampler.DynResampler());
        }

        private ModulationPtr ModulationPtr()
        {
            unsafe
            {
                fixed (byte* pBuf = &_buf[0])
                    return _resample.HasValue ?
                    NativeMethodsBase.AUTDModulationCustomWithResample(LoopBehavior, pBuf, (ushort)_buf.Length, _resample.Value.Item1, _config, _resample.Value.Item2)
                     : NativeMethodsBase.AUTDModulationCustom(_config, LoopBehavior, pBuf, (ushort)_buf.Length);
            }
        }
    }
}
