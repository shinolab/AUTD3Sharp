using System;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp
{
    [Builder]
    public sealed partial class SilencerFixedUpdateRate : IDatagram
    {
        private readonly byte _valueIntensity;
        private readonly byte _valuePhase;

        [Property]
        public SilencerTarget Target { get; private set; }

        public SilencerFixedUpdateRate(byte valueIntensity, byte valuePhase)
        {
            _valueIntensity = valueIntensity;
            _valuePhase = valuePhase;
            Target = SilencerTarget.Intensity;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSilencerFromUpdateRate(_valueIntensity, _valuePhase, Target.Into());
    }

    [Builder]
    public sealed partial class SilencerFixedCompletionTime : IDatagram
    {
        private readonly TimeSpan _valueIntensity;
        private readonly TimeSpan _valuePhase;

        [Property]
        public bool StrictMode { get; private set; }

        [Property]
        public SilencerTarget Target { get; private set; }

        public SilencerFixedCompletionTime(TimeSpan valueIntensity, TimeSpan valuePhase)
        {
            _valueIntensity = valueIntensity;
            _valuePhase = valuePhase;
            StrictMode = true;
            Target = SilencerTarget.Intensity;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSilencerFromCompletionTime((ulong)(_valueIntensity.TotalMilliseconds * 1000 * 1000), (ulong)(_valuePhase.TotalMilliseconds * 1000 * 1000), StrictMode, Target.Into());
    }

    public sealed class Silencer
    {

        public static SilencerFixedUpdateRate FromUpdateRate(byte valueIntensity, byte valuePhase) => new(valueIntensity, valuePhase);
        public static SilencerFixedCompletionTime FromCompletionTime(TimeSpan valueIntensity, TimeSpan valuePhase) => new(valueIntensity, valuePhase);
        public static SilencerFixedCompletionTime Disable() => new(TimeSpan.FromMicroseconds(25), TimeSpan.FromMicroseconds(25));
        public static SilencerFixedCompletionTime Default() => new(TimeSpan.FromMicroseconds(250), TimeSpan.FromMicroseconds(1000));
    }
}
