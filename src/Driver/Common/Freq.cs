using System;
using System.Diagnostics.CodeAnalysis;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public readonly struct Freq<T> : IEquatable<Freq<T>> where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        public T Hz { get; }

        public Freq(T hz) => Hz = hz;

        public override string ToString() => $"{Hz}Hz";

        public static bool operator ==(Freq<T> left, Freq<T> right) => left.Equals(right);
        public static bool operator !=(Freq<T> left, Freq<T> right) => !left.Equals(right);
        public bool Equals(Freq<T> other) => Hz.Equals(other.Hz);
        public override bool Equals(object? obj) => obj is Freq<T> other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Hz.GetHashCode();
    }

    public class UnitHz
    {
        internal UnitHz() { }
        public static Freq<float> operator *(float a, UnitHz _) => new(a);
        public static Freq<uint> operator *(uint a, UnitHz _) => new(a);
    }

    public class UnitkHz
    {
        internal UnitkHz() { }
        public static Freq<float> operator *(float a, UnitkHz _) => new(a * 1000.0f);
        public static Freq<uint> operator *(uint a, UnitkHz _) => new(a * 1000);
    }

    public static partial class Units
    {
        public static UnitHz Hz { get; } = new();
#pragma warning disable IDE1006
        public static UnitkHz kHz { get; } = new();
#pragma warning restore IDE1006
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
