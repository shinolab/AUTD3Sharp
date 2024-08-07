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
        public delegate ushort PulseWidthEncoderDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx, ushort idx);

        private readonly PulseWidthEncoderDelegate? _f;
        private readonly ConcurrentDictionary<ushort, Func<ushort, ushort>>? _cache;

        public PulseWidthEncoder(Func<Device, Func<ushort, ushort>> f)
        {
            _cache = new ConcurrentDictionary<ushort, Func<ushort, ushort>>();
            _f = (_, geometryPtr, devIdx, idx) =>
            {
                var h = _cache.GetOrAdd(devIdx, f(new Device(devIdx, geometryPtr)));
                return h(idx);
            };
        }

        public PulseWidthEncoder()
        {
            _f = null;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry)
        {
            return _f == null ? NativeMethodsBase.AUTDDatagramPulseWidthEncoderDefault() : NativeMethodsBase.AUTDDatagramPulseWidthEncoder(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.Ptr);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
