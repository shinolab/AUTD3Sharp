using System;
using System.ComponentModel;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Derive;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

#if UNITY_2018_3_OR_NEWER
namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class IsExternalInit
    {
    }
}
#endif

namespace AUTD3Sharp
{
    public interface IWithSampling
    {
        internal SamplingConfig SamplingConfigIntensity();
        internal SamplingConfig? SamplingConfigPhase();
    }

    public interface ISilencer
    {
        internal bool IsValid(IWithSampling c, bool strictMode, SilencerTarget target);
        internal DatagramPtr RawPtr(bool strictMode, SilencerTarget target);
    }

    public readonly struct FixedCompletionTime : ISilencer
    {
        public TimeSpan Intensity { get; init; }
        public TimeSpan Phase { get; init; }

        bool ISilencer.IsValid(IWithSampling c, bool strictMode, SilencerTarget target)
           => NativeMethodsBase.AUTDDatagramSilencerFixedCompletionTimeIsValid(((ISilencer)this).RawPtr(strictMode, target), c.SamplingConfigIntensity().Inner, (c.SamplingConfigPhase() ?? new SamplingConfig(0xFFFF)).Inner);

        DatagramPtr ISilencer.RawPtr(bool strictMode, SilencerTarget target)
           => NativeMethodsBase.AUTDDatagramSilencerFromCompletionTime((ulong)(Intensity.TotalMilliseconds * 1000 * 1000), (ulong)(Phase.TotalMilliseconds * 1000 * 1000), strictMode, target.Into());
    }

    public readonly struct FixedUpdateRate : ISilencer
    {
        public ushort Intensity { get; init; }
        public ushort Phase { get; init; }

        bool ISilencer.IsValid(IWithSampling c, bool strictMode, SilencerTarget target)
           => NativeMethodsBase.AUTDDatagramSilencerFixedUpdateRateIsValid(((ISilencer)this).RawPtr(strictMode, target), c.SamplingConfigIntensity().Inner, (c.SamplingConfigPhase() ?? new SamplingConfig(0xFFFF)).Inner);

        DatagramPtr ISilencer.RawPtr(bool strictMode, SilencerTarget target) => NativeMethodsBase.AUTDDatagramSilencerFromUpdateRate(Intensity, Phase, target.Into());
    }

    [Builder]
    public sealed partial class Silencer : IDatagram
    {
        private readonly ISilencer _silencer;

        [Property]
        public bool StrictMode { get; private set; }

        [Property]
        public SilencerTarget Target { get; private set; }

        public Silencer(ISilencer silencer)
        {
            _silencer = silencer;
            StrictMode = true;
            Target = SilencerTarget.Intensity;
        }

        public Silencer() : this(new FixedCompletionTime
        {
            Intensity = TimeSpan.FromMilliseconds(250e-3),
            Phase = TimeSpan.FromMilliseconds(1)
        })
        { }

        public static Silencer Disable() => new(new FixedCompletionTime { Intensity = TimeSpan.FromMilliseconds(25e-3), Phase = TimeSpan.FromMilliseconds(25e-3) });

        public bool IsValid(IWithSampling target) => _silencer.IsValid(target, StrictMode, Target);
        DatagramPtr IDatagram.Ptr(Geometry geometry) => _silencer.RawPtr(StrictMode, Target);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
