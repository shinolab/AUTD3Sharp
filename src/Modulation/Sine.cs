#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using AUTD3Sharp.NativeMethods;

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Sine wave modulation
    /// </summary>
    public sealed class Sine : Driver.Datagram.ModulationWithSamplingConfig<Sine>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="freq">Frequency of sine wave</param>
        /// <remarks>The sine wave is defined as `amp / 2 * sin(2π * freq * t) + offset`, where `t` is time, and `amp = EmitIntensity.Max`, `offset = EmitIntensity.Max/2` by default.</remarks>
        public Sine(float_t freq) : base(SamplingConfiguration.FromFrequency(4000))
        {
            Freq = freq;
            Intensity = EmitIntensity.Max;
            Offset = EmitIntensity.Max / 2;
            Phase = new Phase(0);
            Mode = SamplingMode.ExactFrequency;
        }

        /// <summary>
        /// Set intensity
        /// </summary>
        /// <param name="intensity">Intensity</param>
        /// <returns></returns>
        public Sine WithIntensity(EmitIntensity intensity)
        {
            Intensity = intensity;
            return this;
        }

        /// <summary>
        /// Set intensity
        /// </summary>
        /// <param name="intensity">Intensity</param>
        /// <returns></returns>
        public Sine WithIntensity(byte intensity)
        {
            Intensity = new EmitIntensity(intensity);
            return this;
        }

        /// <summary>
        /// Set offset
        /// </summary>
        /// <param name="offset">Offset of the sine wave</param>
        /// <returns></returns>
        public Sine WithOffset(byte offset)
        {
            Offset = new EmitIntensity(offset);
            return this;
        }

        /// <summary>
        /// Set offset
        /// </summary>
        /// <param name="offset">Offset of the sine wave</param>
        /// <returns></returns>
        public Sine WithOffset(EmitIntensity offset)
        {
            Offset = offset;
            return this;
        }

        /// <summary>
        /// Set phase
        /// </summary>
        /// <param name="phase"> phase</param>
        /// <returns></returns>
        public Sine WithPhase(Phase phase)
        {
            Phase = phase;
            return this;
        }

        /// <summary>
        /// Set sampling mode
        /// </summary>
        /// <param name="mode">sampling mode</param>
        /// <returns></returns>
        public Sine WithMode(SamplingMode mode)
        {
            Mode = mode;
            return this;
        }

        public float_t Freq { get; }

        public EmitIntensity Intensity { get; private set; }

        public EmitIntensity Offset { get; private set; }

        public Phase Phase { get; private set; }

        public SamplingMode Mode { get; private set; }


        public static Fourier operator +(Sine a, Sine b)
            => new Fourier(a).AddComponent(b);

        internal override ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationSine(Freq, Config.Internal, Intensity.Value, Offset.Value, Phase.Value, Mode);
    }
}
