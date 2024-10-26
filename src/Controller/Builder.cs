#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private AUTD3[] _devices;

        [Property]
        public ushort FallbackParallelThreshold { get; private set; } = 4;

        [Property]
        public TimeSpan FallbackTimeout { get; private set; } = TimeSpan.FromMilliseconds(20);

        [Property]
        public TimeSpan SendInterval { get; private set; } = TimeSpan.FromMilliseconds(1);

        [Property]
        public TimeSpan ReceiveInterval { get; private set; } = TimeSpan.FromMilliseconds(1);

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
                fixed (Vector3* pp = &pos[0])
                fixed (Quaternion* rp = &rot[0])
                {
                    return NativeMethodsBase.AUTDControllerBuilder(pp, rp, (ushort)pos.Length,
                        FallbackParallelThreshold,
                        (ulong)FallbackTimeout.TotalMilliseconds * 1000 * 1000,
                        (ulong)SendInterval.TotalMilliseconds * 1000 * 1000,
                        (ulong)ReceiveInterval.TotalMilliseconds * 1000 * 1000,
                        TimerStrategy
                        );
                }
            }
        }

        public async Task<Controller<T>> OpenAsync<T>(ILinkBuilder<T> linkBuilder, TimeSpan? timeout = null)
        {
            var runtime = NativeMethodsBase.AUTDCreateRuntime();
            var handle = NativeMethodsBase.AUTDGetRuntimeHandle(runtime);
            var future = NativeMethodsBase.AUTDControllerOpen(Ptr(), linkBuilder.Ptr(),
                (long)(timeout?.TotalMilliseconds * 1000 * 1000 ?? -1));
            var result = await Task.Run(() => NativeMethodsBase.AUTDWaitResultController(handle, future));
            var ptr = result.Validate();
            var geometry = new Geometry(NativeMethodsBase.AUTDGeometry(ptr));
            var link = linkBuilder.ResolveLink(runtime, NativeMethodsBase.AUTDLinkGet(ptr));
            return new Controller<T>(geometry, runtime, handle, ptr, link);
        }

        public Controller<T> Open<T>(ILinkBuilder<T> linkBuilder, TimeSpan? timeout = null)
        {
            var runtime = NativeMethodsBase.AUTDCreateRuntime();
            var handle = NativeMethodsBase.AUTDGetRuntimeHandle(runtime);
            var future = NativeMethodsBase.AUTDControllerOpen(Ptr(), linkBuilder.Ptr(),
                (long)(timeout?.TotalMilliseconds * 1000 * 1000 ?? -1));
            var ptr = NativeMethodsBase.AUTDWaitResultController(handle, future).Validate();
            var geometry = new Geometry(NativeMethodsBase.AUTDGeometry(ptr));
            var link = linkBuilder.ResolveLink(runtime, NativeMethodsBase.AUTDLinkGet(ptr));
            return new Controller<T>(geometry, runtime, handle, ptr, link);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
