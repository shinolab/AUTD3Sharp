#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    public static class EmissionConstraint
    {
        public static EmissionConstraintWrap Normalize => NativeMethodsGainHolo.AUTDGainHoloConstraintNormalize();
        public static EmissionConstraintWrap Uniform(EmitIntensity value) => Uniform(value.Value);
        public static EmissionConstraintWrap Uniform(byte value) => NativeMethodsGainHolo.AUTDGainHoloConstraintUniform(value);
        public static EmissionConstraintWrap Clamp(EmitIntensity min, EmitIntensity max) => Clamp(min.Value, max.Value);
        public static EmissionConstraintWrap Clamp(byte min, byte max) => NativeMethodsGainHolo.AUTDGainHoloConstraintClamp(min, max);
        public static EmissionConstraintWrap Multiply(float value) => NativeMethodsGainHolo.AUTDGainHoloConstraintMultiply(value);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
