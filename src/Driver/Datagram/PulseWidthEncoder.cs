using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using System.Runtime.InteropServices;
using System;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public sealed class PulseWidthEncoder : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ushort PulseWidthEncoderDelegate(IntPtr context, GeometryPtr geometryPtr, ushort devIdx, ushort idx);

        private readonly PulseWidthEncoderDelegate? _f;

        public PulseWidthEncoder(Func<Device, Func<ushort, ushort>> f)
        {
            _f = (context, geometryPtr, devIdx, idx) => f(new Device(devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx)))(idx);
        }

        public PulseWidthEncoder()
        {
            _f = null;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry)
        {
            return _f == null ? NativeMethodsBase.AUTDDatagramPulseWidthEncoderDefault() : NativeMethodsBase.AUTDDatagramPulseWidthEncoder(Marshal.GetFunctionPointerForDelegate(_f), new ContextPtr { Item1 = IntPtr.Zero }, geometry.Ptr);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
