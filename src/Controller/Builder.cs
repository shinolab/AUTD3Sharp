#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Threading.Tasks;
using AUTD3Sharp.Driver;
using AUTD3Sharp.Utils;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public class ControllerBuilder
    {
        private ControllerBuilderPtr _ptr = NativeMethodsBase.AUTDControllerBuilder();






        public ControllerBuilder AddDevice(AUTD3 device)
        {
            var rot = device.Rot ?? Quaterniond.Identity;
            _ptr = NativeMethodsBase.AUTDControllerBuilderAddDevice(_ptr, device.Pos.X, device.Pos.Y, device.Pos.Z,
                rot.W, rot.X, rot.Y, rot.Z);
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
