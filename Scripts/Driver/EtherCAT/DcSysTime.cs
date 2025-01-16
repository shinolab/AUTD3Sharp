using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DcSysTime
    {
        public ulong SysTime { get; }

        private DcSysTime(ulong sysTime)
        {
            SysTime = sysTime;
        }

        public static DcSysTime Now => NativeMethodsBase.AUTDDcSysTimeNow();

        public static DcSysTime operator +(DcSysTime a, Duration b) => new(a.SysTime + b.AsNanos());
        public static DcSysTime operator -(DcSysTime a, Duration b) => new(a.SysTime - b.AsNanos());
    }
}