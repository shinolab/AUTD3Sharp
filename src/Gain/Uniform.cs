using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    [Gain]
    [Builder]
    public sealed partial class Uniform
    {
        public Uniform(byte intensity)
        {
            Intensity = new EmitIntensity(intensity);
            Phase = new Phase(0);
        }

        public Uniform(EmitIntensity intensity)
        {
            Intensity = intensity;
            Phase = new Phase(0);
        }

        public EmitIntensity Intensity { get; }

        [Property(Phase = true)]
        public Phase Phase { get; private set; }

        private GainPtr GainPtr(Geometry _) => NativeMethodsBase.AUTDGainUniform(Intensity.Value, Phase.Value);
    }
}
