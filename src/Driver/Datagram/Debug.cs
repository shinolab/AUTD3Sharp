using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{
    public class DebugType
    {
        internal DebugTypeWrap Inner;

        private DebugType(DebugTypeWrap inner) { Inner = inner; }

        public static DebugType None => new(NativeMethodsBase.AUTDDebugTypeNone());
        public static DebugType BaseSignal => new(NativeMethodsBase.AUTDDebugTypeBaseSignal());
        public static DebugType Thermo => new(NativeMethodsBase.AUTDDebugTypeThermo());
        public static DebugType ForceFan => new(NativeMethodsBase.AUTDDebugTypeForceFan());
        public static DebugType Sync => new(NativeMethodsBase.AUTDDebugTypeSync());
        public static DebugType ModSegment => new(NativeMethodsBase.AUTDDebugTypeModSegment());
        public static DebugType ModIdx(ushort idx) => new(NativeMethodsBase.AUTDDebugTypeModIdx(idx));
        public static DebugType StmSegment => new(NativeMethodsBase.AUTDDebugTypeStmSegment());
        public static DebugType StmIdx(ushort idx) => new(NativeMethodsBase.AUTDDebugTypeStmIdx(idx));
        public static DebugType IsStmMode => new(NativeMethodsBase.AUTDDebugTypeIsStmMode());
        public static DebugType SysTimeEq(DcSysTime sysTime) => new(NativeMethodsBase.AUTDDebugTypeSysTimeEq(sysTime));
        public static DebugType PwmOut(Transducer tr) => new(NativeMethodsBase.AUTDDebugTypePwmOut(tr.Ptr));
        public static DebugType Direct(bool v) => new(NativeMethodsBase.AUTDDebugTypeDirect(v));
    }

    public sealed class GPIOOutputs : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal unsafe delegate void GPIOOutputsDelegate(IntPtr context, GeometryPtr geometryPtr, ushort devIdx, NativeMethods.GPIOOut gpio, DebugTypeWrap* debugType);

        private readonly GPIOOutputsDelegate _f;

        public GPIOOutputs(Func<Device, GPIOOut, DebugType> f)
        {
            unsafe
            {
                _f = (_, geometryPtr, devIdx, gpio, debugType) => { *debugType = f(new Device(devIdx, geometryPtr), gpio.ToManaged()).Inner; };
            }
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramGPIOOutputs(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.GeometryPtr);
    }
}
