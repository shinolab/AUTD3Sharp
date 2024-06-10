using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Custom
    {
        private readonly byte[] _buf;

        public Custom(byte[] buf, SamplingConfigWrap config)
        {
            _buf = buf;
            _config = config;
        }

        private ModulationPtr ModulationPtr(Geometry _)
        {
            unsafe
            {
                fixed (byte* pBuf = &_buf[0])
                    return NativeMethodsBase.AUTDModulationRaw(_config, LoopBehavior, pBuf, (ushort)_buf.Length);
            }
        }
    }
}
