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
            Intensity = 0xFF;
            Offset = 0xFF / 2;
            Phase = 0 * rad;
            Mode = mode;
        }

        public Sine(Freq<uint> freq) : this(new SamplingModeExact(freq))
        {
        }

        public Sine(Freq<float> freq) : this(new SamplingModeExactFloat(freq))
        {
        }

        public static Sine Nearest(Freq<float> freq) => new(new SamplingModeNearest(freq));

        public Freq<float> Freq => Mode.SineFreq(ModulationPtr());

        [Property]
        public byte Intensity { get; private set; }


        [Property]
        public byte Offset { get; private set; }

        [Property]

        public Angle Phase { get; private set; }

        internal ISamplingMode Mode { get; }

        private ModulationPtr ModulationPtr() => Mode.SinePtr(_config, Intensity, Offset, Phase, LoopBehavior);
    }
}
