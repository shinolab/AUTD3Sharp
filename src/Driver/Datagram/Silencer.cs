using System;
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

            DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSilencerFromUpdateRate(_valueIntensity, _valuePhase);
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

            DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSilencerFromCompletionSteps(_valueIntensity, _valuePhase, _strictMode);
        }

        public sealed class SilencerFixedCompletionTime : IDatagram
        {
            private readonly TimeSpan _valueIntensity;
            private readonly TimeSpan _valuePhase;
            private bool _strictMode;

            public SilencerFixedCompletionTime(TimeSpan valueIntensity, TimeSpan valuePhase)
            {
                _valueIntensity = valueIntensity;
                _valuePhase = valuePhase;
                _strictMode = true;
            }

            public SilencerFixedCompletionTime WithStrictMode(bool mode)
            {
                _strictMode = mode;
                return this;
            }

            DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSilencerFromCompletionTime((ulong)(_valueIntensity.TotalMilliseconds * 1000 * 1000), (ulong)(_valuePhase.TotalMilliseconds * 1000 * 1000), _strictMode);
        }

        public static SilencerFixedUpdateRate FromUpdateRate(ushort valueIntensity, ushort valuePhase) => new SilencerFixedUpdateRate(valueIntensity, valuePhase);
        public static SilencerFixedCompletionSteps FromCompletionSteps(ushort valueIntensity, ushort valuePhase) => new SilencerFixedCompletionSteps(valueIntensity, valuePhase);
        public static SilencerFixedCompletionTime FromCompletionTime(TimeSpan valueIntensity, TimeSpan valuePhase) => new SilencerFixedCompletionTime(valueIntensity, valuePhase);
        public static SilencerFixedCompletionSteps Disable() => new SilencerFixedCompletionSteps(1, 1);
        public static SilencerFixedCompletionSteps Default() => new SilencerFixedCompletionSteps(10, 40);
    }
}
