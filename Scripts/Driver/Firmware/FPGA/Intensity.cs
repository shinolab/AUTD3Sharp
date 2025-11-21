using System;
using System.Diagnostics.CodeAnalysis;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public class Intensity : IEquatable<Intensity>
    {
        internal readonly NativeMethods.Intensity Inner;

        public static readonly Intensity Max = new(0xFF);
        public static readonly Intensity Min = new(0x00);

        public Intensity(byte value)
        {
            Inner.Item1 = value;
        }

        public byte Item1 => Inner.Item1;

        public static Intensity operator /(Intensity a, int b) => new((byte)(a.Inner.Item1 / b));

        public static bool operator ==(Intensity left, Intensity right) => left.Equals(right);
        public static bool operator !=(Intensity left, Intensity right) => !left.Equals(right);
        public bool Equals(Intensity? other) => other is not null && Inner.Item1.Equals(other.Inner.Item1);
        public override bool Equals(object? obj) => obj is Intensity other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Inner.Item1.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
