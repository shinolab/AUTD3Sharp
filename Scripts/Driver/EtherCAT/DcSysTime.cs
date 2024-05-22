using System;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public class DcSysTime
    {
        public ulong SysTime { get; }

        public static DcSysTime Now => new DcSysTime(NativeMethodsBase.AUTDDcSysTimeNow());

        public static DcSysTime operator +(DcSysTime a, TimeSpan b) => new DcSysTime(a.SysTime + (ulong)(b.TotalSeconds * 1000 * 1000 * 1000));
        public static DcSysTime operator -(DcSysTime a, TimeSpan b) => new DcSysTime(a.SysTime - (ulong)(b.TotalSeconds * 1000 * 1000 * 1000));

        private DcSysTime(ulong sysTime)
        {
            SysTime = sysTime;
        }
    }
}