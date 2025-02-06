using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    public sealed class Custom : IModulation
    {
        public byte[] Buffer;
        public SamplingConfig Config;

        public Custom(byte[] buffer, SamplingConfig config)
        {
            Config = config;
            Buffer = buffer;
        }

        ModulationPtr IModulation.ModulationPtr()
        {
            unsafe
            {
                fixed (byte* pBuf = &Buffer[0])
                    return NativeMethodsBase.AUTDModulationCustom(pBuf, (ushort)Buffer.Length, Config.Inner);
            }
        }
    }
}
