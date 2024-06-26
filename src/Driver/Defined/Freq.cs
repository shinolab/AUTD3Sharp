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
        public static Freq<float> operator *(float a, UnitHz _) => new Freq<float>(a);
        public static Freq<uint> operator *(uint a, UnitHz _) => new Freq<uint>(a);
    }

    public class UnitkHz
    {
        internal UnitkHz() { }
        public static Freq<float> operator *(float a, UnitkHz _) => new Freq<float>(a * 1000.0f);
        public static Freq<uint> operator *(uint a, UnitkHz _) => new Freq<uint>(a * 1000);
    }

    public static partial class Units
    {
        public static UnitHz Hz { get; } = new UnitHz();
#pragma warning disable IDE1006
        public static UnitkHz kHz { get; } = new UnitkHz();
#pragma warning restore IDE1006
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
