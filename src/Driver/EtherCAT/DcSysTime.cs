using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

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
        [ExcludeFromCodeCoverage] public override int GetHashCode() => SysTime.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
