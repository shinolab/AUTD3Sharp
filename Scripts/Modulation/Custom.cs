using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    public sealed class Custom : Driver.Datagram.Modulation
    {
        public byte[] Buffer;
        public SamplingConfig Config;

        public Custom(byte[] buffer, SamplingConfig samplingConfig)
        {
            Config = samplingConfig;
            Buffer = buffer;
        }

        internal override ModulationPtr ModulationPtr()
        {
            unsafe
            {
                fixed (byte* pBuf = &Buffer[0])
                    return NativeMethodsBase.AUTDModulationCustom(pBuf, (ushort)Buffer.Length, Config.Inner);
            }
        }
    }
}
