using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

namespace AUTD3Sharp
{
    public sealed class Silencer
    {
        public sealed class SilencerFixedUpdateRate : IDatagram
        {
            private readonly ushort _valueIntensity;
            private readonly ushort _valuePhase;

            public SilencerFixedUpdateRate(ushort valueIntensity, ushort valuePhase)
            {
                _valueIntensity = valueIntensity;
                _valuePhase = valuePhase;
            }

            DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSilencerFixedUpdateRate(_valueIntensity, _valuePhase).Validate();
        }

        public sealed class SilencerFixedCompletionSteps : IDatagram
        {
            private readonly ushort _valueIntensity;
            private readonly ushort _valuePhase;
            private bool _strictMode;

            public SilencerFixedCompletionSteps(ushort valueIntensity, ushort valuePhase)
            {
                _valueIntensity = valueIntensity;
                _valuePhase = valuePhase;
                _strictMode = true;
            }

            public SilencerFixedCompletionSteps WithStrictMode(bool mode)
            {
                _strictMode = mode;
                return this;
            }

            DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSilencerFixedCompletionSteps(_valueIntensity, _valuePhase, _strictMode).Validate();
        }

        public static SilencerFixedUpdateRate FixedUpdateRate(ushort valueIntensity, ushort valuePhase) => new SilencerFixedUpdateRate(valueIntensity, valuePhase);
        public static SilencerFixedCompletionSteps FixedCompletionSteps(ushort valueIntensity, ushort valuePhase) => new SilencerFixedCompletionSteps(valueIntensity, valuePhase);
        public static SilencerFixedCompletionSteps Disable() => new SilencerFixedCompletionSteps(1, 1);
        public static SilencerFixedCompletionSteps Default() => new SilencerFixedCompletionSteps(10, 40);
    }
}
