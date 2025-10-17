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
        private delegate bool ReadsFPGAStateDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx);

        private readonly ReadsFPGAStateDelegate _f;

        public ReadsFPGAState(Func<Device, bool> f) => _f = (_, geometryPtr, devIdx) => f(new Device(devIdx, geometryPtr));

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramReadsFPGAState(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.GeometryPtr);
    }
}
