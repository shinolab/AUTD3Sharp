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
    /// Square wave modulation
    /// </summary>
    public sealed class Square : Driver.Datagram.ModulationWithSamplingConfig<Square>
    {
        public Square(float_t freq) : base(SamplingConfiguration.FromFrequency(4000))
        {
            Freq = freq;
            Low = EmitIntensity.Min;
            High = EmitIntensity.Max;
            Duty = (float_t)0.5;
            Mode = SamplingMode.ExactFrequency;
        }

        /// <summary>
        /// Set low level amplitude
        /// </summary>
        /// <param name="low">low level intensity</param>
        /// <returns></returns>
        public Square WithLow(byte low)
        {
            Low = new EmitIntensity(low);
            return this;
        }

        /// <summary>
        /// Set low level amplitude
        /// </summary>
        /// <param name="low">low level intensity</param>
        /// <returns></returns>
        public Square WithLow(EmitIntensity low)
        {
            Low = low;
            return this;
        }

        /// <summary>
        /// Set high level amplitude
        /// </summary>
        /// <param name="high">high level intensity</param>
        /// <returns></returns>
        public Square WithHigh(byte high)
        {
            High = new EmitIntensity(high);
            return this;
        }

        /// <summary>
        /// Set high level amplitude
        /// </summary>
        /// <param name="high">high level intensity</param>
        /// <returns></returns>
        public Square WithHigh(EmitIntensity high)
        {
            High = high;
            return this;
        }

        /// <summary>
        /// Set duty ratio
        /// </summary>
        /// <remarks>Duty ratio is defined as `Th / (Th + Tl)`, where `Th` is high level duration, and `Tl` is low level duration.</remarks>
        /// <param name="duty">normalized amplitude (0.0 - 1.0)</param>
        /// <returns></returns>
        public Square WithDuty(float_t duty)
        {
            Duty = duty;
            return this;
        }

        /// <summary>
        /// Set sampling mode
        /// </summary>
        /// <param name="mode">sampling mode</param>
        /// <returns></returns>
        public Square WithMode(SamplingMode mode)
        {
            Mode = mode;
            return this;
        }

        public float_t Freq { get; }

        public EmitIntensity Low { get; private set; }

        public EmitIntensity High { get; private set; }

        public float_t Duty { get; private set; }

        public SamplingMode Mode { get; private set; }

        internal override ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationSquare(Freq, Config.Internal, Low.Value, High.Value, Duty, Mode);
    }
}
