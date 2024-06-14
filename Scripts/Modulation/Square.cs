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
            Low = EmitIntensity.Min;
            High = EmitIntensity.Max;
            Duty = 0.5f;
            Mode = mode;
        }

        public Square(Freq<uint> freq) : this(new SamplingModeExact(freq)) { }
        public Square(Freq<float> freq) : this(new SamplingModeExactFloat(freq)) { }
        public static Square WithFreqNearest(Freq<float> freq) => new Square(new SamplingModeNearest(freq));

        [Property(EmitIntensity = true)]
        public EmitIntensity Low { get; private set; }

        [Property(EmitIntensity = true)]
        public EmitIntensity High { get; private set; }

        [Property]

        public float Duty { get; private set; }

        private ISamplingMode Mode { get; }

        private ModulationPtr ModulationPtr(Geometry _) => Mode.SquarePtr(_config, Low, High, Duty, LoopBehavior);
    }
}
