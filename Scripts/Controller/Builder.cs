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

        public ControllerBuilder WithUltrasoundFreq(Freq<uint> freq)
        {
            _ptr = NativeMethodsBase.AUTDControllerBuilderWithUltrasoundFreq(_ptr, freq.Hz);
            return this;
        }

        public ControllerBuilder WithParallelThreshold(ushort threshold)
        {
            _ptr = NativeMethodsBase.AUTDControllerBuilderWithParallelThreshold(_ptr, threshold);
            return this;
        }

        public async Task<Controller<T>> OpenAsync<T>(ILinkBuilder<T> linkBuilder, TimeSpan? timeout = null)
        {
            var ptr = await Task.Run(() => NativeMethodsBase.AUTDControllerOpen(_ptr, linkBuilder.Ptr(), (long)(timeout?.TotalMilliseconds * 1000 * 1000 ?? -1)).Validate());
            var geometry = new Geometry(NativeMethodsBase.AUTDGeometry(ptr));
            var link = linkBuilder.ResolveLink(NativeMethodsBase.AUTDLinkGet(ptr));
            return new Controller<T>(geometry, ptr, link);
        }

        public Controller<T> Open<T>(ILinkBuilder<T> linkBuilder, TimeSpan? timeout = null)
        {
            var ptr = NativeMethodsBase.AUTDControllerOpen(_ptr, linkBuilder.Ptr(), (long)(timeout?.TotalMilliseconds * 1000 * 1000 ?? -1)).Validate();
            var geometry = new Geometry(NativeMethodsBase.AUTDGeometry(ptr));
            var link = linkBuilder.ResolveLink(NativeMethodsBase.AUTDLinkGet(ptr));
            return new Controller<T>(geometry, ptr, link);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
