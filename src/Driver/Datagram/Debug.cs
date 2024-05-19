using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{
    public static class DebugType
    {
        public static DebugTypeWrap None => NativeMethodsBase.AUTDDebugTypeNone();
        public static DebugTypeWrap BaseSignal => NativeMethodsBase.AUTDDebugTypeBaseSignal();
        public static DebugTypeWrap Thermo => NativeMethodsBase.AUTDDebugTypeThermo();
        public static DebugTypeWrap ForceFan => NativeMethodsBase.AUTDDebugTypeForceFan();
        public static DebugTypeWrap Sync => NativeMethodsBase.AUTDDebugTypeSync();
        public static DebugTypeWrap ModSegment => NativeMethodsBase.AUTDDebugTypeModSegment();
        public static DebugTypeWrap ModIdx(ushort idx) => NativeMethodsBase.AUTDDebugTypeModIdx(idx);
        public static DebugTypeWrap StmSegment => NativeMethodsBase.AUTDDebugTypeStmSegment();
        public static DebugTypeWrap StmIdx(ushort idx) => NativeMethodsBase.AUTDDebugTypeModIdx(idx);
        public static DebugTypeWrap IsStmMode => NativeMethodsBase.AUTDDebugTypeIsStmMode();
        public static DebugTypeWrap PwmOut(Transducer tr) => NativeMethodsBase.AUTDDebugTypePwmOut(tr.Ptr);
        public static DebugTypeWrap Direct(bool v) => NativeMethodsBase.AUTDDebugTypeDirect(v);
    }

    public sealed class DebugSettings : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public unsafe delegate void DebugSettingsDelegate(IntPtr context, GeometryPtr geometryPtr, uint devIdx, GPIOOut gpio, DebugTypeWrap* debugType);

        private readonly DebugSettingsDelegate _f;

        public DebugSettings(Func<Device, GPIOOut, DebugTypeWrap> f)
        {
            unsafe
            {
                _f = (context, geometryPtr, devIdx, gpio, debugType) =>
                {
                    *debugType = f(new Device((int)devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx)), gpio);
                };
            }
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramDebugSettings(Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, geometry.Ptr);
    }
}
