using System;
using System.Diagnostics.CodeAnalysis;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public readonly struct Freq<T> : IEquatable<Freq<T>> where T : struct
    {
        public T Hz { get; }

        public Freq(T hz)
        {
            Hz = hz;
        }

        [ExcludeFromCodeCoverage]
        public bool Equals(Freq<T> other)
        {
            return Hz.Equals(other.Hz);
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object? obj)
        {
            return obj is Freq<T> other && Equals(other);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return Hz.GetHashCode();
        }
    }

    public class UnitHz
    {
        internal UnitHz() { }
        public static Freq<double> operator *(double a, UnitHz _) => new Freq<double>(a);
        public static Freq<uint> operator *(uint a, UnitHz _) => new Freq<uint>(a);
    }

    public class UnitkHz
    {
        internal UnitkHz() { }
        public static Freq<double> operator *(double a, UnitkHz _) => new Freq<double>(a * 1000.0);
        public static Freq<uint> operator *(uint a, UnitkHz _) => new Freq<uint>(a * 1000);
    }

    public static partial class Units
    {
        public static UnitHz Hz { get; } = new UnitHz();
        public static UnitkHz kHz { get; } = new UnitkHz();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
