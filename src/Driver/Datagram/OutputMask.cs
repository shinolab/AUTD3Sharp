using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using System.Runtime.InteropServices;
using System;
using System.Collections.Concurrent;

namespace AUTD3Sharp
{
    public sealed class OutputMask : IDatagram, IDatagramS
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        private delegate bool OutputMaskDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx, byte idx);

        private readonly OutputMaskDelegate _f;

        public OutputMask(Func<Device, Func<Transducer, bool>> f)
        {
            ConcurrentDictionary<ushort, Func<Transducer, bool>> cache = new();
            _f = (_, geometryPtr, devIdx, idx) =>
            {
                var dev = new Device(devIdx, geometryPtr);
                return cache.GetOrAdd(devIdx, f(dev))(new Transducer(idx, devIdx, dev.Ptr));
            };
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramOutputMask(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.GeometryPtr);

        DatagramPtr IDatagramS.WithSegmentTransition(Geometry geometry, Segment segment, TransitionMode? _transitionMode) => NativeMethodsBase.AUTDDatagramOutputMaskWithSegment(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.GeometryPtr, segment.ToNative());
    }
}
