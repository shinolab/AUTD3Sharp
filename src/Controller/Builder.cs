#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AUTD3Sharp.Driver;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public class ControllerBuilder
    {
        private ControllerBuilderPtr _ptr;

        internal ControllerBuilder(IEnumerable<AUTD3> iter)
        {
            var devIter = iter as AUTD3[] ?? iter.ToArray();
            var pos = devIter.Select(dev => dev.Pos).ToArray();
            var rot = devIter.Select(dev => dev.Rotation).ToArray();
            unsafe
            {
                fixed (Vector3* pp = &pos[0])
                fixed (Quaternion* rp = &rot[0])
                {
                    _ptr = NativeMethodsBase.AUTDControllerBuilder(pp, rp, (ushort)pos.Length);
                }
            }
        }

        public ControllerBuilder WithSendInterval(TimeSpan interval)
        {
            _ptr = NativeMethodsBase.AUTDControllerBuilderWithSendInterval(_ptr, (ulong)(interval.TotalMilliseconds * 1000 * 1000));
            return this;
        }

        public ControllerBuilder WithReceiveInterval(TimeSpan interval)
        {
            _ptr = NativeMethodsBase.AUTDControllerBuilderWithReceiveInterval(_ptr, (ulong)(interval.TotalMilliseconds * 1000 * 1000));
            return this;
        }

        public ControllerBuilder WithTimerResolution(uint? resolution)
        {
            _ptr = NativeMethodsBase.AUTDControllerBuilderWithTimerResolution(_ptr, resolution ?? 0);
            return this;
        }

        public ControllerBuilder WithParallelThreshold(ushort threshold)
        {
            _ptr = NativeMethodsBase.AUTDControllerBuilderWithParallelThreshold(_ptr, threshold);
            return this;
        }

        public async Task<Controller<T>> OpenAsync<T>(ILinkBuilder<T> linkBuilder, TimeSpan? timeout = null)
        {
            var runtime = NativeMethodsBase.AUTDCreateRuntime();
            var handle = NativeMethodsBase.AUTDGetRuntimeHandle(runtime);
            var future = NativeMethodsBase.AUTDControllerOpen(_ptr, linkBuilder.Ptr(),
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
            var future = NativeMethodsBase.AUTDControllerOpen(_ptr, linkBuilder.Ptr(),
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
