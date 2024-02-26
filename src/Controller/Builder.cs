#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#define DIMENSION_M
#endif

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Threading.Tasks;
using AUTD3Sharp.Driver;

using AUTD3Sharp.NativeMethods;

#if UNITY_2018_3_OR_NEWER
using Quaternion = UnityEngine.Quaternion;
#else
using Quaternion = AUTD3Sharp.Utils.Quaterniond;
#endif

namespace AUTD3Sharp
{
    public class ControllerBuilder
    {
        private ControllerBuilderPtr _ptr = NativeMethodsBase.AUTDControllerBuilder();

        /// <summary>
        /// Add device
        /// </summary>
        /// <param name="device">AUTD3 device</param>
        /// <returns></returns>
        public ControllerBuilder AddDevice(AUTD3 device)
        {
            var rot = device.Rot ?? Quaternion.identity;
            _ptr = NativeMethodsBase.AUTDControllerBuilderAddDevice(_ptr, device.Pos.x, device.Pos.y, device.Pos.z,
                rot.w, rot.x, rot.y, rot.z);
            return this;
        }

        /// <summary>
        /// Open controller
        /// </summary>
        /// <param name="linkBuilder">link</param>
        /// <param name="timeout">timeout</param>
        /// <returns>Controller</returns>
        public async Task<Controller<T>> OpenAsync<T>(ILinkBuilder<T> linkBuilder, TimeSpan? timeout = null)
        {
            var ptr = await Task.Run(() => NativeMethodsBase.AUTDControllerOpen(_ptr, linkBuilder.Ptr(), (long)(timeout?.TotalMilliseconds * 1000 * 1000 ?? -1)).Validate());
            var geometry = new Geometry(NativeMethodsBase.AUTDGeometry(ptr));
            var link = linkBuilder.ResolveLink(NativeMethodsBase.AUTDLinkGet(ptr));
            return new Controller<T>(geometry, ptr, link);
        }

        /// <summary>
        /// Open controller
        /// </summary>
        /// <param name="linkBuilder">link</param>
        /// <param name="timeout">timeout</param>
        /// <returns>Controller</returns>
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
