#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#define DIMENSION_M
#endif

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public sealed class GroupGuard<T>
    {
        private readonly Controller<T> _controller;
        private readonly Func<Device, object?> _map;
        private GroupKVMapPtr _kvMap;
        private readonly IDictionary<object, int> _keymap;
        private int _k;

        internal GroupGuard(Func<Device, object?> map, Controller<T> controller)
        {
            _controller = controller;
            _map = map;
            _kvMap = NativeMethodsBase.AUTDControllerGroupCreateKVMap();
            _keymap = new Dictionary<object, int>();
            _k = 0;
        }

        public GroupGuard<T> Set(object key, IDatagram data1, IDatagram data2, TimeSpan? timeout = null)
        {
            if (_keymap.ContainsKey(key)) throw new AUTDException("Key already exists");

            var timeoutNs = (long)(timeout?.TotalMilliseconds * 1000 * 1000 ?? -1);
            var ptr1 = data1.Ptr(_controller.Geometry);
            var ptr2 = data2.Ptr(_controller.Geometry);
            _keymap[key] = _k++;
            _kvMap = NativeMethodsBase.AUTDControllerGroupKVMapSet(_kvMap, _keymap[key], ptr1, ptr2, timeoutNs).Validate();
            return this;
        }

        public GroupGuard<T> Set(object key, IDatagram data, TimeSpan? timeout = null)
        {
            return Set(key, data, new NullDatagram(), timeout);
        }

        public GroupGuard<T> Set(object key, (IDatagram, IDatagram) data, TimeSpan? timeout = null)
        {
            return Set(key, data.Item1, data.Item2, timeout);
        }

        public async Task<bool> SendAsync()
        {
            var map = _controller.Geometry.Select(dev =>
            {
                if (!dev.Enable) return -1;
                var k = _map(dev);
                return k != null ? _keymap[k] : -1;
            }).ToArray();

            return await Task.Run(() =>
            {
                unsafe
                {
                    fixed (int* mp = &map[0])
                        return NativeMethodsBase.AUTDControllerGroup(_controller.Ptr, mp, _kvMap).Validate() == NativeMethodsDef.AUTD3_TRUE;
                }
            });
        }

        public bool Send()
        {
            var map = _controller.Geometry.Select(dev =>
            {
                if (!dev.Enable) return -1;
                var k = _map(dev);
                return k != null ? _keymap[k] : -1;
            }).ToArray();
            unsafe
            {
                fixed (int* mp = &map[0])
                {
                    return NativeMethodsBase.AUTDControllerGroup(_controller.Ptr, mp, _kvMap).Validate() == NativeMethodsDef.AUTD3_TRUE;
                }
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
