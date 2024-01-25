using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Geometry;
using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{
    /// <summary>
    /// Datagram to set modulation delay
    /// </summary>
    public sealed class ConfigureModDelay : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate ushort ConfigureModDelayDelegate(IntPtr context, GeometryPtr geometryPtr, uint devIdx, byte trIdx);

        private readonly ConfigureModDelayDelegate _f;

        public ConfigureModDelay(Func<Device, Transducer, ushort> f)
        {
            _f = (context, geometryPtr, devIdx, trIdx) =>
            {
                var dev = new Device((int)devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx));
                var tr = new Transducer(trIdx, dev.Ptr);
                return f(dev, tr);
            };
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramConfigureModDelay(Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, geometry.Ptr);
    }
}
