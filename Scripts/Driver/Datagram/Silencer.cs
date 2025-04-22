using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp
{
    public interface ISilencer
    {
        internal DatagramPtr RawPtr();
    }

    public class FixedCompletionTime : ISilencer
    {
        public Duration Intensity { get; init; } = Duration.FromMicros(250);
        public Duration Phase { get; init; } = Duration.FromMillis(1);
        public bool StrictMode { get; init; } = true;

        DatagramPtr ISilencer.RawPtr() => NativeMethodsBase.AUTDDatagramSilencerFromCompletionTime(new NativeMethods.FixedCompletionTime
        {
            intensity = Intensity,
            phase = Phase,
            strict_mode = StrictMode
        });
    }

    public class FixedCompletionSteps : ISilencer
    {
        public ushort Intensity { get; init; } = 10;
        public ushort Phase { get; init; } = 40;
        public bool StrictMode { get; init; } = true;

        internal NativeMethods.FixedCompletionSteps ToNative() => new()
        {
            intensity = Intensity,
            phase = Phase,
            strict_mode = StrictMode
        };

        DatagramPtr ISilencer.RawPtr() => NativeMethodsBase.AUTDDatagramSilencerFromCompletionSteps(ToNative());
    }

    public class FixedUpdateRate : ISilencer
    {
        public ushort Intensity { get; init; }
        public ushort Phase { get; init; }

        internal NativeMethods.FixedUpdateRate ToNative() => new()
        {
            intensity = Intensity,
            phase = Phase
        };

        DatagramPtr ISilencer.RawPtr() => NativeMethodsBase.AUTDDatagramSilencerFromUpdateRate(ToNative());
    }

    public sealed class Silencer : IDatagram
    {
        public ISilencer Config = new FixedCompletionSteps();

        public Silencer(ISilencer config)
        {
            Config = config;
        }

        public Silencer() { }

        public static Silencer Disable() => new(new FixedCompletionSteps { Intensity = 1, Phase = 1 });

        DatagramPtr IDatagram.Ptr(Geometry geometry) => Config.RawPtr();
    }
}
