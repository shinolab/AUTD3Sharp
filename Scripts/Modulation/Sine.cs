using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Sine wave modulation
    /// </summary>
    [Modulation]
    [Builder]
    public sealed partial class Sine
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="freq">Frequency of sine wave</param>
        /// <remarks>The sine wave is defined as `amp / 2 * sin(2π * freq * t) + offset`, where `t` is time, and `amp = EmitIntensity.Max`, `offset = EmitIntensity.Max/2` by default.</remarks>
        public Sine(double freq)
        {
            Freq = freq;
            Intensity = EmitIntensity.Max;
            Offset = EmitIntensity.Max / 2;
            Phase = new Phase(0);
            Mode = SamplingMode.ExactFrequency;
        }

        public double Freq { get; }

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }


        [Property(EmitIntensity = true)]
        public EmitIntensity Offset { get; private set; }

        [Property]

        public Phase Phase { get; private set; }

        [Property]

        public SamplingMode Mode { get; private set; }

        public static Fourier operator +(Sine a, Sine b)
            => new Fourier(a).AddComponent(b);

        private ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationSine(Freq, _config.Internal, Intensity.Value, Offset.Value, Phase.Value, Mode, LoopBehavior.Internal);
    }
}
