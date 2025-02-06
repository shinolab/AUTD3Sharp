using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct DcSysTime : IEquatable<DcSysTime>
    {
        public ulong SysTime { get; }

        private DcSysTime(ulong sysTime) => SysTime = sysTime;

        public static DcSysTime Now => NativeMethodsBase.AUTDDcSysTimeNow();

        public static DcSysTime operator +(DcSysTime a, Duration b) => new(a.SysTime + b.AsNanos());
        public static DcSysTime operator -(DcSysTime a, Duration b) => new(a.SysTime - b.AsNanos());
        public static bool operator ==(DcSysTime left, DcSysTime right) => left.Equals(right);
        public static bool operator !=(DcSysTime left, DcSysTime right) => !left.Equals(right);
        public bool Equals(DcSysTime other) => SysTime.Equals(other.SysTime);
        public override bool Equals(object? obj) => obj is DcSysTime other && Equals(other);
        public override int GetHashCode() => SysTime.GetHashCode();
    }
}
