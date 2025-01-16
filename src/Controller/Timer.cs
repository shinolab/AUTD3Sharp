#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Timer
{
    public class StdSleeper
    {
        public uint? TimerResolution { get; init; }

        [ExcludeFromCodeCoverage]
        public StdSleeper() : this(1) { }

        [ExcludeFromCodeCoverage]
        public StdSleeper(uint? timerResolution)
        {
            TimerResolution = timerResolution;
        }
    }

    public class AsyncSleeper
    {
        public uint? TimerResolution { get; init; }

        [ExcludeFromCodeCoverage]
        public AsyncSleeper() : this(1) { }

        [ExcludeFromCodeCoverage]
        public AsyncSleeper(uint? timerResolution)
        {
            TimerResolution = timerResolution;
        }
    }

    [ExcludeFromCodeCoverage]
    public readonly struct WaitableSleeper
    {
    }

    [Builder]
    public partial class SpinSleeper
    {
        public uint NativeAccuracyNs { get; }

        [Property]
        public SpinStrategyTag SpinStrategy { get; private set; }

        [ExcludeFromCodeCoverage]
        public SpinSleeper(uint nativeAccuracyNs)
        {
            NativeAccuracyNs = nativeAccuracyNs;
            SpinStrategy = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? SpinStrategyTag.SpinLoopHint : SpinStrategyTag.YieldThread;
        }

        public SpinSleeper() : this(NativeMethodsBase.AUTDTimerStrategySpinDefaultAccuracy())
        {
        }
    }

    public class TimerStrategy
    {
        public static TimerStrategyWrap Std(StdSleeper sleeper) => NativeMethodsBase.AUTDTimerStrategyStd(sleeper.TimerResolution ?? 0);
        public static TimerStrategyWrap Spin(SpinSleeper sleeper) => NativeMethodsBase.AUTDTimerStrategySpin(sleeper.NativeAccuracyNs, sleeper.SpinStrategy);
        [ExcludeFromCodeCoverage]
        public static TimerStrategyWrap Waitable(WaitableSleeper sleeper) => NativeMethodsBase.AUTDTimerStrategyWaitable();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
