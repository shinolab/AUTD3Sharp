using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{
    public sealed class ReadsFPGAState : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public delegate bool ReadsFPGAStateDelegate(IntPtr context, GeometryPtr geometryPtr, ushort devIdx);

        private readonly ReadsFPGAStateDelegate _f;

        public ReadsFPGAState(Func<Device, bool> f)
        {
            _f = (context, geometryPtr, devIdx) => f(new Device(devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx)));
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramReadsFPGAState(Marshal.GetFunctionPointerForDelegate(_f), new ContextPtr { Item1 = IntPtr.Zero }, geometry.Ptr);
    }
}
