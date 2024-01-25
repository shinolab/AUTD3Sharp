using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Geometry;
using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{
    /// <summary>
    /// Datagram to configure reads FPGA Info
    /// </summary>
    public sealed class ConfigureReadsFPGAState : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public delegate bool ConfigureReadsFPGAStateDelegate(IntPtr context, GeometryPtr geometryPtr, uint devIdx);

        private readonly ConfigureReadsFPGAStateDelegate _f;

        public ConfigureReadsFPGAState(Func<Device, bool> f)
        {
            _f = (context, geometryPtr, devIdx) => f(new Device((int)devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx)));
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramConfigureReadsFPGAState(Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, geometry.Ptr);
    }
}
