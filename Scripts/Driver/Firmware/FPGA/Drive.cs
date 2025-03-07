using System;
using System.Diagnostics.CodeAnalysis;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#nullable enable
#endif

namespace AUTD3Sharp
{
    public readonly struct Drive : IEquatable<Drive>
    {
        public Phase Phase { get; init; }
        public EmitIntensity Intensity { get; init; }

        public Drive(Phase phase, EmitIntensity intensity)
        {
            Phase = phase;
            Intensity = intensity;
        }

        public static Drive Null => new()
        {
            Intensity = EmitIntensity.Min,
            Phase = Phase.Zero
        };

        internal NativeMethods.Drive ToNative() => new()
        {
            phase = Phase.Inner,
            intensity = Intensity.Inner
        };

        public static bool operator ==(Drive left, Drive right) => left.Equals(right);
        public static bool operator !=(Drive left, Drive right) => !left.Equals(right);
        public bool Equals(Drive other) => Phase.Equals(other.Phase) && Intensity.Equals(other.Intensity);
        public override bool Equals(object? obj) => obj is Drive other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => HashCode.Combine(Phase, Intensity);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
