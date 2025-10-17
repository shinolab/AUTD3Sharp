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
    public sealed class OutputMask : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        private delegate bool OutputMaskDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx, byte idx);

        private readonly OutputMaskDelegate _f;
        private readonly Segment _segment;

        private OutputMask(Func<Device, Func<Transducer, bool>> f, Segment segment)
        {
            ConcurrentDictionary<ushort, Func<Transducer, bool>> cache = new();
            _f = (_, geometryPtr, devIdx, idx) =>
            {
                var dev = new Device(devIdx, geometryPtr);
                return cache.GetOrAdd(devIdx, f(dev))(new Transducer(idx, devIdx, dev.Ptr));
            };
            _segment = segment;
        }

        public OutputMask(Func<Device, Func<Transducer, bool>> f) : this(f, Segment.S0) { }

        public static OutputMask WithSegment(Func<Device, Func<Transducer, bool>> f, Segment segment) => new(f, segment);

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramOutputMaskWithSegment(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.GeometryPtr, _segment.ToNative());
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
