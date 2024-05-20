using System;

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

        public bool Equals(Freq<T> other)
        {
            return Hz.Equals(other.Hz);
        }

        public override bool Equals(object? obj)
        {
            return obj is Freq<T> other && Equals(other);
        }

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

    public static partial class Units
    {
        public static UnitHz Hz { get; } = new UnitHz();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
