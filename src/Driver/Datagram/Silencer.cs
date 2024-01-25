using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Geometry;

namespace AUTD3Sharp
{
    /// <summary>
    /// Datagram to configure silencer
    /// </summary>
    public sealed class ConfigureSilencer
    {
        public sealed class ConfigureSilencerFixedUpdateRate : IDatagram
        {
            private readonly ushort _valueIntensity;
            private readonly ushort _valuePhase;

            public ConfigureSilencerFixedUpdateRate(ushort valueIntensity, ushort valuePhase)
            {
                _valueIntensity = valueIntensity;
                _valuePhase = valuePhase;
            }

            DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSilencerFixedUpdateRate(_valueIntensity, _valuePhase).Validate();
        }

        public sealed class ConfigureSilencerFixedCompletionSteps : IDatagram
        {
            private readonly ushort _valueIntensity;
            private readonly ushort _valuePhase;
            private bool _strictMode;

            public ConfigureSilencerFixedCompletionSteps(ushort valueIntensity, ushort valuePhase)
            {
                _valueIntensity = valueIntensity;
                _valuePhase = valuePhase;
                _strictMode = true;
            }

            /// <summary>
            /// Set strict mode
            /// </summary>
            /// <param name="mode">strict mode</param>
            /// <returns></returns>
            public ConfigureSilencerFixedCompletionSteps WithStrictMode(bool mode)
            {
                _strictMode = mode;
                return this;
            }

            DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSilencerFixedCompletionSteps(_valueIntensity, _valuePhase, _strictMode).Validate();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="valueIntensity">Intensity update rate of silencer. The smaller step is, the quieter the output is.</param>
        /// <param name="valuePhase">Phase update rate of silencer. The smaller step is, the quieter the output is.</param>
        public static ConfigureSilencerFixedUpdateRate FixedUpdateRate(ushort valueIntensity, ushort valuePhase) => new ConfigureSilencerFixedUpdateRate(valueIntensity, valuePhase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="valueIntensity">Intensity update rate of silencer. The smaller step is, the quieter the output is.</param>
        /// <param name="valuePhase">Phase update rate of silencer. The smaller step is, the quieter the output is.</param>
        public static ConfigureSilencerFixedCompletionSteps FixedCompletionSteps(ushort valueIntensity, ushort valuePhase) => new ConfigureSilencerFixedCompletionSteps(valueIntensity, valuePhase);

        /// <summary>
        /// Disable silencer
        /// </summary>
        /// <returns></returns>
        public static ConfigureSilencerFixedCompletionSteps Disable() => new ConfigureSilencerFixedCompletionSteps(1, 1);

        /// <summary>
        /// Default silencer
        /// </summary>
        /// <returns></returns>
        public static ConfigureSilencerFixedCompletionSteps Default() => new ConfigureSilencerFixedCompletionSteps(10, 40);
    }
}
