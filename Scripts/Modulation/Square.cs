#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Square wave modulation
    /// </summary>
    [Modulation]
    [Builder]
    public sealed partial class Square
    {
        public Square(float_t freq)
        {
            Freq = freq;
            Low = EmitIntensity.Min;
            High = EmitIntensity.Max;
            Duty = (float_t)0.5;
            Mode = SamplingMode.ExactFrequency;
        }

        public float_t Freq { get; }

        [Property(EmitIntensity = true)]
        public EmitIntensity Low { get; private set; }

        [Property(EmitIntensity = true)]
        public EmitIntensity High { get; private set; }

        [Property]

        public float_t Duty { get; private set; }


        [Property]
        public SamplingMode Mode { get; private set; }

        private ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationSquare(Freq, _config.Internal, Low.Value, High.Value, Duty, Mode, LoopBehavior.Internal);
    }
}
