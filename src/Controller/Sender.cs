using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public interface ISleeper
    {
        internal SleeperWrap ToNative();
    }

    public class StdSleeper : ISleeper
    {
        public uint? TimerResolution = 1;

        SleeperWrap ISleeper.ToNative() => new()
        {
            tag = SleeperTag.Std,
            value = TimerResolution ?? 0,
            spin_strategy = NativeMethods.SpinStrategyTag.SpinLoopHint
        };
    }

    [ExcludeFromCodeCoverage]
    public readonly struct WaitableSleeper : ISleeper
    {
        SleeperWrap ISleeper.ToNative() => new()
        {
            tag = SleeperTag.Waitable,
            value = 0,
            spin_strategy = NativeMethods.SpinStrategyTag.SpinLoopHint
        };
    }

    public class SpinSleeper : ISleeper
    {
        public uint NativeAccuracyNs = NativeMethodsBase.AUTDSpinSleepDefaultAccuracy();
        public SpinStrategyTag SpinStrategy = DefaultTag();

        [ExcludeFromCodeCoverage]
        private static SpinStrategyTag DefaultTag() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? SpinStrategyTag.SpinLoopHint : SpinStrategyTag.YieldThread;

        SleeperWrap ISleeper.ToNative() => new()
        {
            tag = SleeperTag.Spin,
            value = NativeAccuracyNs,
            spin_strategy = SpinStrategy.ToNative()
        };
    }

    public class SenderOption
    {
        public Duration SendInterval = Duration.FromMillis(1);
        public Duration ReceiveInterval = Duration.FromMillis(1);
        public Duration? Timeout = null;
        public ParallelMode Parallel = ParallelMode.Auto;
        public ISleeper Sleeper = new SpinSleeper();

        internal NativeMethods.SenderOption ToNative() => new()
        {
            send_interval = SendInterval,
            receive_interval = ReceiveInterval,
            timeout = Timeout.ToNative(),
            parallel = Parallel.ToNative(),
            sleeper = Sleeper.ToNative()
        };
    }

    public partial class Sender
    {
        internal SenderPtr Ptr;
        internal readonly Geometry Geometry;

        internal Sender(SenderPtr ptr, Geometry geometry)
        {
            Ptr = ptr;
            Geometry = geometry;
        }

        public void Send<TD>(TD d) where TD : IDatagram => NativeMethodsBase.AUTDSenderSend(Ptr, d.Ptr(Geometry)).Validate();
        public void Send<TD1, TD2>((TD1, TD2) d) where TD1 : IDatagram where TD2 : IDatagram => Send(new DatagramTuple<TD1, TD2>(d));
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
