#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AUTD3Sharp.Driver;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public class ControllerBuilder
    {
        private ControllerBuilderPtr _ptr;

        public ControllerBuilder(IEnumerable<AUTD3> iter)
        {
            var devices = iter.SelectMany(dev => new[] { dev.Pos.X, dev.Pos.Y, dev.Pos.Z, dev.Rot.W, dev.Rot.X, dev.Rot.Y, dev.Rot.Z }).ToArray();
            unsafe
            {
                fixed (float* p = &devices[0])
                {
                    _ptr = NativeMethodsBase.AUTDControllerBuilder(p, (ushort)(devices.Length / 7));
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
