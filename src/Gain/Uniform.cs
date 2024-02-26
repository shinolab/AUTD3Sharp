#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif


using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    /// <summary>
    /// Gain to set amp and phase uniformly
    /// </summary>
    /// 
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

        [Property]
        public Phase Phase { get; private set; }

        private GainPtr GainPtr(Geometry geometry) => NativeMethodsBase.AUTDGainUniform(Intensity.Value, Phase.Value);
    }
}
