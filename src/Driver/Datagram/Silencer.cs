using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp
{
    public interface ISilencer
    {
        internal DatagramPtr RawPtr(SilencerTarget target);
    }

    public readonly struct FixedCompletionTime : ISilencer
    {
        public Duration Intensity { get; init; } = Duration.FromMicros(250);
        public Duration Phase { get; init; } = Duration.FromMillis(1);
        public bool StrictMode { get; init; } = true;

        public FixedCompletionTime() { }

        DatagramPtr ISilencer.RawPtr(SilencerTarget target) => NativeMethodsBase.AUTDDatagramSilencerFromCompletionTime(new NativeMethods.FixedCompletionTime
        {
            intensity = Intensity,
            phase = Phase,
            strict_mode = StrictMode
        }, target);
    }

    public readonly struct FixedCompletionSteps : ISilencer
    {
        public ushort Intensity { get; init; } = 10;
        public ushort Phase { get; init; } = 40;
        public bool StrictMode { get; init; } = true;

        public FixedCompletionSteps() { }

        internal NativeMethods.FixedCompletionSteps ToNative() => new()
        {
            intensity = Intensity,
            phase = Phase,
            strict_mode = StrictMode
        };

        DatagramPtr ISilencer.RawPtr(SilencerTarget target) => NativeMethodsBase.AUTDDatagramSilencerFromCompletionSteps(ToNative(), target);
    }

    public readonly struct FixedUpdateRate : ISilencer
    {
        public ushort Intensity { get; init; }
        public ushort Phase { get; init; }

        internal NativeMethods.FixedUpdateRate ToNative() => new()
        {
            intensity = Intensity,
            phase = Phase
        };

        DatagramPtr ISilencer.RawPtr(SilencerTarget target) => NativeMethodsBase.AUTDDatagramSilencerFromUpdateRate(ToNative(), target);
    }

    public sealed class Silencer : IDatagram
    {
        public ISilencer Config = new FixedCompletionSteps();
        public SilencerTarget Target = SilencerTarget.Intensity;

        public Silencer(ISilencer config, SilencerTarget target)
        {
            Config = config;
            Target = target;
        }

        public Silencer() { }

        public static Silencer Disable() => new(new FixedCompletionSteps { Intensity = 1, Phase = 1 }, SilencerTarget.Intensity);

        DatagramPtr IDatagram.Ptr(Geometry geometry) => Config.RawPtr(Target);
    }
}
