using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Geometry;
using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{
    /// <summary>
    /// Datagram to configure force fan
    /// </summary>
    public sealed class ConfigureForceFan : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public delegate bool ConfigureForceFanDelegate(IntPtr context, GeometryPtr geometryPtr, uint devIdx);

        private readonly ConfigureForceFanDelegate _f;

        public ConfigureForceFan(Func<Device, bool> f)
        {
            _f = (context, geometryPtr, devIdx) => f(new Device((int)devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx)));
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramConfigureForceFan(Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, geometry.Ptr);
    }
}
