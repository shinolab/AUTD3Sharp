using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public sealed class PulseWidthEncoder : IDatagram
    {
        private readonly ushort[]? _buf;

        public PulseWidthEncoder(ushort[] buf)
        {
            _buf = buf;
        }

        public PulseWidthEncoder()
        {
            _buf = null;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry)
        {
            if (_buf == null) return NativeMethodsBase.AUTDDatagramPulseWidthEncoderDefault();
            unsafe
            {
                fixed (ushort* p = &_buf[0])
                {
                    return NativeMethodsBase.AUTDDatagramPulseWidthEncoder(p, (uint)_buf.Length).Validate();
                }
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
