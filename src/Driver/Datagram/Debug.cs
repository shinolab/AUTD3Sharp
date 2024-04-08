using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

using System.Runtime.InteropServices;
using System;
using System.Linq;

namespace AUTD3Sharp
{
    public class DebugType
    {
        internal byte _ty;
        internal ushort _value;

        private DebugType(byte ty, ushort value)
        {
            _ty = ty;
            _value = value;
        }

        public static DebugType None() => new DebugType(0x00, 0x0000);
        public static DebugType BaseSignal() => new DebugType(0x01, 0x0000);
        public static DebugType Thermo() => new DebugType(0x02, 0x0000);
        public static DebugType ForceFan() => new DebugType(0x03, 0x0000);
        public static DebugType Sync() => new DebugType(0x10, 0x0000);
        public static DebugType ModSegment() => new DebugType(0x20, 0x0000);
        public static DebugType ModIdx(ushort idx) => new DebugType(0x21, idx);
        public static DebugType StmSegment() => new DebugType(0x50, 0x0000);
        public static DebugType StmIdx(ushort idx) => new DebugType(0x51, idx);
        public static DebugType IsStmMode() => new DebugType(0x52, 0x0000);
        public static DebugType PwmOut(Transducer tr) => new DebugType(0xE0, (ushort)tr.Idx);
        public static DebugType Direct(bool v) => new DebugType(0xF0, v ? (ushort)0x0001 : (ushort)0x0000);
    }


    /// <summary>
    /// Datagram to configure debug output
    /// </summary>
    public sealed class ConfigureDebugSettings : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate DebugSettings DebugSettingsDelegate(IntPtr context, GeometryPtr geometryPtr, uint devIdx);

        private readonly DebugSettingsDelegate _f;

        public ConfigureDebugSettings(Func<Device, DebugType[]> f)
        {
            _f = (context, geometryPtr, devIdx) =>
            {
                var v = f(new Device((int)devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx)));
                var res = new DebugSettings();
                unsafe
                {
                    res.ty[0] = v[0]._ty;
                    res.ty[1] = v[1]._ty;
                    res.ty[2] = v[2]._ty;
                    res.ty[3] = v[3]._ty;
                    res.value[0] = v[0]._value;
                    res.value[1] = v[1]._value;
                    res.value[2] = v[2]._value;
                    res.value[3] = v[3]._value;
                }
                return res;
            };
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramConfigureDebugSettings(Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, geometry.Ptr);
    }
}
