#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Driver;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Timer;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    [Builder]
    public partial class ControllerBuilder
    {
        private readonly AUTD3[] _devices;

        [Property]
        public ushort DefaultParallelThreshold { get; private set; } = 4;

        [Property]
        public Duration DefaultTimeout { get; private set; } = Duration.FromMillis(20);

        [Property]
        public Duration SendInterval { get; private set; } = Duration.FromMillis(1);

        [Property]
        public Duration ReceiveInterval { get; private set; } = Duration.FromMillis(1);

        [Property]
        public TimerStrategyWrap TimerStrategy { get; private set; } = Timer.TimerStrategy.Spin(new SpinSleeper());

        internal ControllerBuilder(IEnumerable<AUTD3> iter)
        {
            _devices = iter as AUTD3[] ?? iter.ToArray();
        }

        private ControllerBuilderPtr Ptr()
        {
            var pos = _devices.Select(dev => dev.Pos).ToArray();
            var rot = _devices.Select(dev => dev.Rotation).ToArray();
            unsafe
            {
                fixed (Point3* pp = &pos[0])
                fixed (Quaternion* rp = &rot[0])
                {
                    return NativeMethodsBase.AUTDControllerBuilder(pp, rp, (ushort)pos.Length,
                        DefaultParallelThreshold,
                        DefaultTimeout,
                        SendInterval,
                        ReceiveInterval,
                        TimerStrategy
                        );
                }
            }
        }

        public Controller<T> Open<T>(ILinkBuilder<T> linkBuilder, Duration? timeout = null)
        {
            var ptr = NativeMethodsBase.AUTDControllerOpen(Ptr(), linkBuilder.Ptr(), timeout.Into()).Validate();
            return new Controller<T>(NativeMethodsBase.AUTDGeometry(ptr), ptr, linkBuilder.ResolveLink(NativeMethodsBase.AUTDLinkGet(ptr)));
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
