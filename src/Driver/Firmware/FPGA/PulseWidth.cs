using System;
using System.Diagnostics.CodeAnalysis;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public class PulseWidth : IEquatable<PulseWidth>
    {
        public readonly ushort Value;

        public PulseWidth(ushort pulseWidth)
        {
            if (512 <= pulseWidth)
                throw new ArgumentOutOfRangeException(nameof(pulseWidth), "PulseWidth must be in range [0, 511]");
            Value = pulseWidth;
        }

        public static PulseWidth FromDuty(float duty) => new((ushort)(512.0f * duty));

        public static bool operator ==(PulseWidth left, PulseWidth right) => left.Equals(right);
        public static bool operator !=(PulseWidth left, PulseWidth right) => !left.Equals(right);
        public bool Equals(PulseWidth? other) => other is not null && Value.Equals(other.Value);
        public override bool Equals(object? obj) => obj is PulseWidth other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Value.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
