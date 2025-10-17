using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using System.Runtime.InteropServices;
using System;
using System.Collections.Concurrent;

namespace AUTD3Sharp
{
    public sealed class PhaseCorrection : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte PhaseCorrectionDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx, byte idx);

        private readonly PhaseCorrectionDelegate _f;

        public PhaseCorrection(Func<Device, Func<Transducer, Phase>> f)
        {
            ConcurrentDictionary<ushort, Func<Transducer, Phase>> cache = new();
            _f = (_, geometryPtr, devIdx, idx) =>
            {
                var dev = new Device(devIdx, geometryPtr);
                return cache.GetOrAdd(devIdx, f(dev))(new Transducer(idx, devIdx, dev.Ptr)).Item1;
            };
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramPhaseCorr(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.GeometryPtr);
    }
}
