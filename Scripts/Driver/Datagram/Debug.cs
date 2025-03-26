using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{
    public class GPIOOutputType
    {
        internal GPIOOutputTypeWrap Inner;

        private GPIOOutputType(GPIOOutputTypeWrap inner) { Inner = inner; }

        public static GPIOOutputType None => new(NativeMethodsBase.AUTDGPIOOutputTypeNone());
        public static GPIOOutputType BaseSignal => new(NativeMethodsBase.AUTDGPIOOutputTypeBaseSignal());
        public static GPIOOutputType Thermo => new(NativeMethodsBase.AUTDGPIOOutputTypeThermo());
        public static GPIOOutputType ForceFan => new(NativeMethodsBase.AUTDGPIOOutputTypeForceFan());
        public static GPIOOutputType Sync => new(NativeMethodsBase.AUTDGPIOOutputTypeSync());
        public static GPIOOutputType ModSegment => new(NativeMethodsBase.AUTDGPIOOutputTypeModSegment());
        public static GPIOOutputType ModIdx(ushort idx) => new(NativeMethodsBase.AUTDGPIOOutputTypeModIdx(idx));
        public static GPIOOutputType StmSegment => new(NativeMethodsBase.AUTDGPIOOutputTypeStmSegment());
        public static GPIOOutputType StmIdx(ushort idx) => new(NativeMethodsBase.AUTDGPIOOutputTypeStmIdx(idx));
        public static GPIOOutputType IsStmMode => new(NativeMethodsBase.AUTDGPIOOutputTypeIsStmMode());
        public static GPIOOutputType SysTimeEq(DcSysTime sysTime) => new(NativeMethodsBase.AUTDGPIOOutputTypeSysTimeEq(sysTime));
        public static GPIOOutputType PwmOut(Transducer tr) => new(NativeMethodsBase.AUTDGPIOOutputTypePwmOut(tr.Ptr));
        public static GPIOOutputType Direct(bool v) => new(NativeMethodsBase.AUTDGPIOOutputTypeDirect(v));
    }

    public sealed class GPIOOutputs : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal unsafe delegate void GPIOOutputsDelegate(IntPtr context, GeometryPtr geometryPtr, ushort devIdx, NativeMethods.GPIOOut gpio, GPIOOutputTypeWrap* debugType);

        private readonly GPIOOutputsDelegate _f;

        public GPIOOutputs(Func<Device, GPIOOut, GPIOOutputType> f)
        {
            unsafe
            {
                _f = (_, geometryPtr, devIdx, gpio, debugType) => { *debugType = f(new Device(devIdx, geometryPtr), gpio.ToManaged()).Inner; };
            }
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramGPIOOutputs(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.GeometryPtr);
    }
}
