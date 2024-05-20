using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{
    public sealed class PhaseFilter : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte PhaseFilterDelegate(IntPtr context, GeometryPtr geometryPtr, uint devIdx, byte trIdx);

        private readonly PhaseFilterDelegate _f;

        private PhaseFilter(Func<Device, Transducer, Phase> f)
        {
            _f = (context, geometryPtr, devIdx, trIdx) =>
            {
                var devPtr = NativeMethodsBase.AUTDDevice(geometryPtr, devIdx);
                return f(new Device((int)devIdx, devPtr), new Transducer(trIdx, devPtr)).Value;
            };
        }

        public static PhaseFilter Additive(Func<Device, Transducer, Phase> f) => new PhaseFilter(f);

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramPhaseFilterAdditive(Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, geometry.Ptr);
    }
}
