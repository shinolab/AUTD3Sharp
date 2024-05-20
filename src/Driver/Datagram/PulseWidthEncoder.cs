using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

namespace AUTD3Sharp
{
    public sealed class PulseWidthEncoder : IDatagram
    {
        private readonly List<ushort>? _buf;

        public PulseWidthEncoder(List<ushort> buf)
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
                fixed (ushort* p = _buf.ToArray())
                {
                    return NativeMethodsBase.AUTDDatagramPulseWidthEncoder(p, (uint)_buf.Count).Validate();
                }
            }
        }
    }
}
