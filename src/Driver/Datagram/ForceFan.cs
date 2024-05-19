using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{



    public sealed class ForceFan : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public delegate bool ForceFanDelegate(IntPtr context, GeometryPtr geometryPtr, uint devIdx);

        private readonly ForceFanDelegate _f;

        public ForceFan(Func<Device, bool> f)
        {
            _f = (context, geometryPtr, devIdx) => f(new Device((int)devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx)));
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramForceFan(Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, geometry.Ptr);
    }
}
