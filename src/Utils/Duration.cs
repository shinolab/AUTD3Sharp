using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AUTD3Sharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Duration : IEquatable<Duration>, IComparable<Duration>
    {
        private ulong _nanos;

        public static Duration Zero => new(0);

        private Duration(ulong nanos)
        {
            _nanos = nanos;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Duration FromNanos(ulong nanos) => new(nanos);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Duration FromMicros(ulong micros) => FromNanos(micros * 1000);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Duration FromMillis(ulong millis) => FromMicros(millis * 1000);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Duration FromSecs(ulong seconds) => FromMillis(seconds * 1000);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong AsNanos() => _nanos;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong AsMicros() => AsNanos() / 1000;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong AsMillis() => AsMicros() / 1000;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong AsSecs() => AsMillis() / 1000;

        public static Duration operator +(Duration a, Duration b) => new(a._nanos + b._nanos);
        public static Duration operator -(Duration a, Duration b) => new(a._nanos - b._nanos);
        public static Duration operator *(Duration a, int b) => new(a._nanos * (ulong)b);
        public static Duration operator *(int a, Duration b) => new((ulong)a * b._nanos);
        public static Duration operator /(Duration a, int b) => new(a._nanos / (ulong)b);
        public static Duration operator %(Duration a, int b) => new(a._nanos % (ulong)b);
        public static bool operator ==(Duration a, Duration b) => a.Equals(b);
        public static bool operator !=(Duration a, Duration b) => !a.Equals(b);
        public static bool operator <(Duration a, Duration b) => a.CompareTo(b) < 0;
        public static bool operator >(Duration a, Duration b) => a.CompareTo(b) > 0;
        public static bool operator <=(Duration a, Duration b) => a.CompareTo(b) <= 0;
        public static bool operator >=(Duration a, Duration b) => a.CompareTo(b) >= 0;

        public bool Equals(Duration other) => _nanos == other._nanos;
        public override bool Equals(object? obj) => obj is Duration other && Equals(other);
        public override int GetHashCode() => _nanos.GetHashCode();

        public override string ToString()
        {
            var ns = AsNanos();
            if (ns < 1000) return $"{ns}ns";
            var us = ns / 1000;
            if (us < 1000) return ns % 1000 != 0 ? $"{us}.{(ns % 1000):D3}".TrimEnd('0') + "μs" : $"{us}μs";
            var ms = us / 1000;
            if (ms < 1000) return us % 1000 != 0 ? $"{ms}.{(us % 1000):D3}".TrimEnd('0') + "ms" : $"{ms}ms";
            var s = ms / 1000;
            return ms % 1000 != 0 ? $"{s}.{(ms % 1000):D3}".TrimEnd('0') + "s" : $"{s}s";
        }

        public int CompareTo(Duration other)
        {
            return _nanos.CompareTo(other._nanos);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OptionDuration
    {
        [MarshalAs(UnmanagedType.U1)] public bool has_value;
        public Duration value;

        public Duration? Into() => has_value ? value : null;
    }

    public static class OptionDurationExt
    {
        public static OptionDuration Into(this Duration? duration) => new OptionDuration { has_value = duration.HasValue, value = duration ?? Duration.Zero };
    }
}
