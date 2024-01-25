#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    /// <summary>
    /// Gain to set amp and phase uniformly
    /// </summary>
    public sealed class Uniform : Driver.Datagram.Gain
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

        /// <summary>
        /// Set phase
        /// </summary>
        /// <param name="phase">phase</param>
        /// <returns></returns>
        public Uniform WithPhase(Phase phase)
        {
            Phase = phase;
            return this;
        }

        public EmitIntensity Intensity { get; }
        public Phase Phase { get; private set; }

        internal override GainPtr GainPtr(Geometry geometry) => NativeMethodsBase.AUTDGainUniform(Intensity.Value, Phase.Value);
    }
}
