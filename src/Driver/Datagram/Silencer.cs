using System;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Derive;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

#if UNITY_2018_3_OR_NEWER
using System.ComponentModel;

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
        internal bool IsValid(IWithSampling c, bool strictMode);
        internal DatagramPtr RawPtr(bool strictMode, SilencerTarget target);
    }

    public readonly struct FixedCompletionTime : ISilencer
    {
        public TimeSpan Intensity { get; init; }
        public TimeSpan Phase { get; init; }

        bool ISilencer.IsValid(IWithSampling c, bool strictMode)
           => NativeMethodsBase.AUTDDatagramSilencerFixedCompletionTimeIsValid((ulong)(Intensity.TotalMilliseconds * 1000 * 1000), (ulong)(Phase.TotalMilliseconds * 1000 * 1000), strictMode,
               c.SamplingConfigIntensity(), c.SamplingConfigPhase() ?? new SamplingConfig(0xFFFF)
               );

        DatagramPtr ISilencer.RawPtr(bool strictMode, SilencerTarget target)
           => NativeMethodsBase.AUTDDatagramSilencerFromCompletionTime((ulong)(Intensity.TotalMilliseconds * 1000 * 1000), (ulong)(Phase.TotalMilliseconds * 1000 * 1000), strictMode, target);
    }

    public readonly struct FixedUpdateRate : ISilencer
    {
        public ushort Intensity { get; init; }
        public ushort Phase { get; init; }

        bool ISilencer.IsValid(IWithSampling c, bool strictMode)
            => NativeMethodsBase.AUTDDatagramSilencerFixedUpdateRateIsValid(Intensity, Phase,
                c.SamplingConfigIntensity(), c.SamplingConfigPhase() ?? new SamplingConfig(0xFFFF)
            );
        DatagramPtr ISilencer.RawPtr(bool strictMode, SilencerTarget target) => NativeMethodsBase.AUTDDatagramSilencerFromUpdateRate(Intensity, Phase, target);
    }

    [Builder]
    public sealed partial class Silencer : IDatagram
    {
        public ISilencer Inner { get; init; }

        [Property]
        public bool StrictMode { get; private set; }

        [Property]
        public SilencerTarget Target { get; private set; }

        public Silencer(ISilencer silencer)
        {
            Inner = silencer;
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

        public bool IsValid(IWithSampling target) => Inner.IsValid(target, StrictMode);
        DatagramPtr IDatagram.Ptr(Geometry geometry) => Inner.RawPtr(StrictMode, Target);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
