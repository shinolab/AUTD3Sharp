using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp.Modulation
{
    [Modulation]
    [Builder]
    public sealed partial class Sine
    {
        private Sine(ISamplingMode mode)
        {
            Intensity = EmitIntensity.Max;
            Offset = EmitIntensity.Max / 2;
            Phase = 0 * rad;
            Mode = mode;
        }

        public Sine(Freq<uint> freq) : this(new SamplingModeExact(freq))
        {
        }

        public Sine(Freq<float> freq) : this(new SamplingModeExactFloat(freq))
        {
        }

        public static Sine WithFreqNearest(Freq<float> freq) => new Sine(new SamplingModeNearest(freq));

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }


        [Property(EmitIntensity = true)]
        public EmitIntensity Offset { get; private set; }

        [Property]

        public Angle Phase { get; private set; }

        internal ISamplingMode Mode { get; }

        private ModulationPtr ModulationPtr(Geometry _) => Mode.SinePtr(_config, Intensity, Offset, Phase, LoopBehavior);
    }
}
