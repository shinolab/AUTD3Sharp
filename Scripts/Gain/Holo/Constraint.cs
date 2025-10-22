using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    public class EmissionConstraint
    {
        internal EmissionConstraintWrap Inner;

        private EmissionConstraint(EmissionConstraintWrap inner) => Inner = inner;

        public static EmissionConstraint Normalize => new(NativeMethodsGainHolo.AUTDGainHoloConstraintNormalize());
        public static EmissionConstraint Uniform(Intensity value) => new(NativeMethodsGainHolo.AUTDGainHoloConstraintUniform(value.Inner));
        public static EmissionConstraint Clamp(Intensity min, Intensity max) => new(NativeMethodsGainHolo.AUTDGainHoloConstraintClamp(min.Inner, max.Inner));
        public static EmissionConstraint Multiply(float value) => new(NativeMethodsGainHolo.AUTDGainHoloConstraintMultiply(value));
    }
}
