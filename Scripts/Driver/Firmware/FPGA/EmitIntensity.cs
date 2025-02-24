using System;
using System.Diagnostics.CodeAnalysis;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public class EmitIntensity : IEquatable<EmitIntensity>
    {
        internal readonly NativeMethods.EmitIntensity Inner;

        public static readonly EmitIntensity Max = new(0xFF);
        public static readonly EmitIntensity Min = new(0x00);

        [ExcludeFromCodeCoverage]
        private EmitIntensity() { }

        public EmitIntensity(byte value)
        {
            Inner.Item1 = value;
        }

        public byte Item1 => Inner.Item1;

        public static EmitIntensity operator /(EmitIntensity a, int b) => new((byte)(a.Inner.Item1 / b));

        public static bool operator ==(EmitIntensity left, EmitIntensity right) => left.Equals(right);
        public static bool operator !=(EmitIntensity left, EmitIntensity right) => !left.Equals(right);
        public bool Equals(EmitIntensity? other) => other is not null && Inner.Equals(other.Inner);
        public override bool Equals(object? obj) => obj is EmitIntensity other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Inner.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
