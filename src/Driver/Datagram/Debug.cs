#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Geometry;
using System.Runtime.InteropServices;
using System;

namespace AUTD3Sharp
{
    /// <summary>
    /// Datagram to configure debug output
    /// </summary>
    public sealed class ConfigureDebugOutputIdx : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate byte DebugOutputDelegate(IntPtr context, GeometryPtr geometryPtr, uint devIdx);

        private readonly DebugOutputDelegate _f;

        public ConfigureDebugOutputIdx(Func<Device, Transducer?> f)
        {
            _f = (context, geometryPtr, devIdx) =>
            {
                var tr = f(new Device((int)devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx)));
                return (byte)(tr?.Idx ?? 0xFF);
            };
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramConfigureDebugOutputIdx(Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, geometry.Ptr);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
