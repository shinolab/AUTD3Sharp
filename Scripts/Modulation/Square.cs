using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [Modulation]
    [Builder]
    public sealed partial class Square
    {
        private Square(ISamplingMode mode)
        {
            Low = 0x00;
            High = 0xFF;
            Duty = 0.5f;
            Mode = mode;
        }

        public Square(Freq<uint> freq) : this(new SamplingModeExact(freq)) { }
        public Square(Freq<float> freq) : this(new SamplingModeExactFloat(freq)) { }
        public static Square FromFreqNearest(Freq<float> freq) => new Square(new SamplingModeNearest(freq));

        [Property]
        public byte Low { get; private set; }

        [Property]
        public byte High { get; private set; }

        [Property]

        public float Duty { get; private set; }

        private ISamplingMode Mode { get; }

        private ModulationPtr ModulationPtr() => Mode.SquarePtr(_config, Low, High, Duty, LoopBehavior);
    }
}
