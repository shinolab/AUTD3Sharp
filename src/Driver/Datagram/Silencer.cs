using System;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp
{
    public interface IWithSampling
    {
        internal SamplingConfig? SamplingConfigIntensity();
        internal SamplingConfig? SamplingConfigPhase();
    }

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

        public bool IsValid(IWithSampling target) => NativeMethodsBase.AUTDDatagramSilencerFixedUpdateRateIsValid(RawPtr(), (target.SamplingConfigIntensity() ?? new SamplingConfig(0xFFFF)).Inner, (target.SamplingConfigPhase() ?? new SamplingConfig(0xFFFF)).Inner);

        private DatagramPtr RawPtr() => NativeMethodsBase.AUTDDatagramSilencerFromUpdateRate(_valueIntensity, _valuePhase, Target.Into());
        DatagramPtr IDatagram.Ptr(Geometry geometry) => RawPtr();
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

        public bool IsValid(IWithSampling target) => NativeMethodsBase.AUTDDatagramSilencerFixedCompletionTimeIsValid(RawPtr(), (target.SamplingConfigIntensity() ?? new SamplingConfig(0xFFFF)).Inner, (target.SamplingConfigPhase() ?? new SamplingConfig(0xFFFF)).Inner);

        private DatagramPtr RawPtr() => NativeMethodsBase.AUTDDatagramSilencerFromCompletionTime((ulong)(_valueIntensity.TotalMilliseconds * 1000 * 1000), (ulong)(_valuePhase.TotalMilliseconds * 1000 * 1000), StrictMode, Target.Into());
        DatagramPtr IDatagram.Ptr(Geometry geometry) => RawPtr();
    }

    public sealed class Silencer
    {

        public static SilencerFixedUpdateRate FromUpdateRate(byte valueIntensity, byte valuePhase) => new(valueIntensity, valuePhase);
        public static SilencerFixedCompletionTime FromCompletionTime(TimeSpan valueIntensity, TimeSpan valuePhase) => new(valueIntensity, valuePhase);
        public static SilencerFixedCompletionTime Disable() => new(TimeSpan.FromMilliseconds(25e-3), TimeSpan.FromMilliseconds(25e-3));
        public static SilencerFixedCompletionTime Default() => new(TimeSpan.FromMilliseconds(250e-3), TimeSpan.FromMilliseconds(1));
    }
}
