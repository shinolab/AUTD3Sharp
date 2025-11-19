using AUTD3Sharp.NativeMethods;
using System.Runtime.CompilerServices;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

[assembly: InternalsVisibleTo("tests")]
namespace AUTD3Sharp
{
    public interface ITransitionMode
    {
        internal TransitionModeWrap Inner { get; }
    }

    public interface IInfiniteTransitionMode : ITransitionMode { }
    public interface IFiniteTransitionMode : ITransitionMode { }

    namespace TransitionMode
    {
        public sealed class Immediate : IInfiniteTransitionMode
        {
            TransitionModeWrap ITransitionMode.Inner { get; } = NativeMethodsBase.AUTDTransitionModeImmediate();
        }

        public sealed class Ext : IInfiniteTransitionMode
        {
            TransitionModeWrap ITransitionMode.Inner { get; } = NativeMethodsBase.AUTDTransitionModeExt();
        }

        public sealed class GPIO : IFiniteTransitionMode
        {
            private readonly GPIOIn _gpio;

            TransitionModeWrap ITransitionMode.Inner => NativeMethodsBase.AUTDTransitionModeGPIO(_gpio.ToNative());

            public GPIO(GPIOIn gpio)
            {
                _gpio = gpio;
            }
        }

        public sealed class SysTime : IFiniteTransitionMode
        {
            private readonly DcSysTime _sysTime;
            TransitionModeWrap ITransitionMode.Inner => NativeMethodsBase.AUTDTransitionModeSysTime(_sysTime);

            public SysTime(DcSysTime sysTime)
            {
                _sysTime = sysTime;
            }
        }

        public sealed class SyncIdx : IFiniteTransitionMode
        {
            TransitionModeWrap ITransitionMode.Inner { get; } = NativeMethodsBase.AUTDTransitionModeSyncIdx();
        }

        public sealed class Later : IInfiniteTransitionMode, IFiniteTransitionMode
        {
            TransitionModeWrap ITransitionMode.Inner { get; } = NativeMethodsBase.AUTDTransitionModeLater();
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
