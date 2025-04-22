using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public class Phase : IEquatable<Phase>
    {
        internal readonly NativeMethods.Phase Inner;

        public static readonly Phase Zero = new(0x00);
        public static readonly Phase Pi = new(0x80);

        [ExcludeFromCodeCoverage]
        private Phase() { }

        public Phase(byte value) => Inner.Item1 = value;
        public Phase(Angle value) : this(NativeMethodsBase.AUTDPhaseFromRad(value.Radian)) { }

        public byte Item1 => Inner.Item1;

        public float Radian() => NativeMethodsBase.AUTDPhaseToRad(Inner);

        public static bool operator ==(Phase left, Phase right) => left.Equals(right);
        public static bool operator !=(Phase left, Phase right) => !left.Equals(right);
        public bool Equals(Phase? other) => other is not null && Inner.Equals(other.Inner);
        public override bool Equals(object? obj) => obj is Phase other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Inner.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
