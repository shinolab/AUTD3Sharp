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
    public sealed class PhaseCorrection : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte PhaseCorrectionDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx, byte idx);

        private readonly PhaseCorrectionDelegate _f;
        private readonly ConcurrentDictionary<ushort, Func<Transducer, Phase>>? _cache;

        public PhaseCorrection(Func<Device, Func<Transducer, Phase>> f)
        {
            _cache = new ConcurrentDictionary<ushort, Func<Transducer, Phase>>();
            _f = (_, geometryPtr, devIdx, idx) =>
            {
                var dev = new Device(devIdx, geometryPtr);
                var tr = new Transducer(idx, devIdx, dev.Ptr);
                var h = _cache.GetOrAdd(devIdx, f(dev));
                return h(tr).Value;
            };
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry)
        {
            return NativeMethodsBase.AUTDDatagramPhaseCorr(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.GeometryPtr);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
