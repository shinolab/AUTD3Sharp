using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using System.Runtime.InteropServices;
using System;
using System.Collections.Concurrent;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public sealed class PulseWidthEncoder : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ushort PulseWidthEncoderDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx, byte idx);

        private readonly PulseWidthEncoderDelegate? _f;

        public PulseWidthEncoder(Func<Device, Func<EmitIntensity, PulseWidth>> f)
        {
            ConcurrentDictionary<ushort, Func<EmitIntensity, PulseWidth>> cache = new();
            _f = (_, geometryPtr, devIdx, idx) => cache.GetOrAdd(devIdx, f(new Device(devIdx, geometryPtr)))(new EmitIntensity(idx)).Value;
        }

        public PulseWidthEncoder() => _f = null;

        DatagramPtr IDatagram.Ptr(Geometry geometry) => _f == null ? NativeMethodsBase.AUTDDatagramPulseWidthEncoderDefault() : NativeMethodsBase.AUTDDatagramPulseWidthEncoder(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.GeometryPtr);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
