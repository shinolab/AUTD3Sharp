using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{
    public sealed class ConfigurePhaseFilter : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte ConfigurePhaseFilterDelegate(IntPtr context, GeometryPtr geometryPtr, uint devIdx, byte trIdx);

        private readonly ConfigurePhaseFilterDelegate _f;

        public ConfigurePhaseFilter(Func<Device, Transducer, Phase> f)
        {
            _f = (context, geometryPtr, devIdx, trIdx) =>
            {
                var devPtr = NativeMethodsBase.AUTDDevice(geometryPtr, devIdx);
                return f(new Device((int)devIdx, devPtr), new Transducer((int)trIdx, devPtr)).Value;
            };
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramConfigurePhaseFilter(Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, geometry.Ptr);
    }
}
