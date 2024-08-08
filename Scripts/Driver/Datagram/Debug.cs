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
        public static DebugTypeWrap StmIdx(ushort idx) => NativeMethodsBase.AUTDDebugTypeStmIdx(idx);
        public static DebugTypeWrap IsStmMode => NativeMethodsBase.AUTDDebugTypeIsStmMode();
        public static DebugTypeWrap PwmOut(Transducer tr) => NativeMethodsBase.AUTDDebugTypePwmOut(tr.Ptr);
        public static DebugTypeWrap Direct(bool v) => NativeMethodsBase.AUTDDebugTypeDirect(v);
    }

    public sealed class DebugSettings : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal unsafe delegate void DebugSettingsDelegate(IntPtr context, GeometryPtr geometryPtr, ushort devIdx, GPIOOut gpio, DebugTypeWrap* debugType);

        private readonly DebugSettingsDelegate _f;

        public DebugSettings(Func<Device, GPIOOut, DebugTypeWrap> f)
        {
            unsafe
            {
                _f = (_, geometryPtr, devIdx, gpio, debugType) =>
                {
                    *debugType = f(new Device(devIdx, geometryPtr), gpio);
                };
            }
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramDebugSettings(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.Ptr);
    }
}
