using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Custom
    {
        private readonly byte[] _buf;

        public Custom(byte[] buf, SamplingConfig config)
        {
            _buf = buf;
            _config = config;
        }

        private ModulationPtr ModulationPtr()
        {
            unsafe
            {
                fixed (byte* pBuf = &_buf[0])
                    return NativeMethodsBase.AUTDModulationRaw(_config.Inner, LoopBehavior, pBuf, (ushort)_buf.Length);
            }
        }
    }
}
