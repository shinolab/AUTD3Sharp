#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Amplitude constraint
    /// </summary>
    public struct EmissionConstraint : IEquatable<EmissionConstraint>
    {
        internal EmissionConstraintPtr Ptr;

        private EmissionConstraint(EmissionConstraintPtr ptr)
        {
            Ptr = ptr;
        }

        /// <summary>
        /// Do nothing (this is equivalent to `Clamp(EmitIntensity.Min, EmitIntensity.Max)`)
        /// </summary>
        public static EmissionConstraint DontCare() =>
            new EmissionConstraint(NativeMethodsGainHolo.AUTDGainHoloConstraintDotCare());

        /// <summary>
        /// Normalize the value by dividing the maximum value
        /// </summary>
        public static EmissionConstraint Normalize() =>
            new EmissionConstraint(NativeMethodsGainHolo.AUTDGainHoloConstraintNormalize());

        /// <summary>
        /// Set all amplitudes to the specified value
        /// </summary>
        public static EmissionConstraint Uniform(EmitIntensity value) =>
            new EmissionConstraint(NativeMethodsGainHolo.AUTDGainHoloConstraintUniform(value.Value));

        /// <summary>
        /// Set all amplitudes to the specified value
        /// </summary>
        public static EmissionConstraint Uniform(byte value) => Uniform(new EmitIntensity(value));

        /// <summary>
        /// Clamp all amplitudes to the specified range
        /// </summary>
        public static EmissionConstraint Clamp(EmitIntensity min, EmitIntensity max) =>
            new EmissionConstraint(NativeMethodsGainHolo.AUTDGainHoloConstraintClamp(min.Value, max.Value));

        /// <summary>
        /// Clamp all amplitudes to the specified range
        /// </summary>
        public static EmissionConstraint Clamp(byte min, byte max) => Clamp(new EmitIntensity(min), new EmitIntensity(max));

        public override bool Equals(object? obj) => obj is EmissionConstraint other && Equals(other);
        public readonly bool Equals(EmissionConstraint other) => NativeMethodsGainHolo.AUTDGainHoloConstraintEq(Ptr, other.Ptr);
        public readonly override int GetHashCode() => Ptr.GetHashCode();
        public static bool operator ==(EmissionConstraint lhs, EmissionConstraint rhs) => lhs.Equals(rhs);
        public static bool operator !=(EmissionConstraint lhs, EmissionConstraint rhs) => !(lhs == rhs);

    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
