using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Square wave modulation
    /// </summary>
    [Modulation]
    [Builder]
    public sealed partial class Square
    {
        public Square(double freq)
        {
            Freq = freq;
            Low = EmitIntensity.Min;
            High = EmitIntensity.Max;
            Duty = 0.5;
            Mode = SamplingMode.ExactFrequency;
        }

        public double Freq { get; }

        [Property(EmitIntensity = true)]
        public EmitIntensity Low { get; private set; }

        [Property(EmitIntensity = true)]
        public EmitIntensity High { get; private set; }

        [Property]

        public double Duty { get; private set; }


        [Property]
        public SamplingMode Mode { get; private set; }

        private ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationSquare(Freq, _config.Internal, Low.Value, High.Value, Duty, Mode, LoopBehavior.Internal);
    }
}
