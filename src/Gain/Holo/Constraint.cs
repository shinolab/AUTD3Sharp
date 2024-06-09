#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    public static class EmissionConstraint
    {
        public static EmissionConstraintWrap DontCare => NativeMethodsGainHolo.AUTDGainHoloConstraintDotCare();
        public static EmissionConstraintWrap Normalize => NativeMethodsGainHolo.AUTDGainHoloConstraintNormalize();
        public static EmissionConstraintWrap Uniform(EmitIntensity value) => NativeMethodsGainHolo.AUTDGainHoloConstraintUniform(value.Value);
        public static EmissionConstraintWrap Clamp(EmitIntensity min, EmitIntensity max) => NativeMethodsGainHolo.AUTDGainHoloConstraintClamp(min.Value, max.Value);
        public static EmissionConstraintWrap Multiply(float value) => NativeMethodsGainHolo.AUTDGainHoloConstraintMultiply(value);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
