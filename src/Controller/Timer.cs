#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Driver;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.FPGA.Defined;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Timer
{
    [Builder]
    public partial class SpinSleeper
    {
        public uint NativeAccuracyNs { get; }

        [Property]
        public SpinStrategyTag SpinStrategy { get; private set; }

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
        public static TimerStrategyWrap Spin(SpinSleeper sleeper) => NativeMethodsBase.AUTDTimerStrategySpin(sleeper.NativeAccuracyNs, sleeper.SpinStrategy);

    }
}