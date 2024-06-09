#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public sealed class GroupGuard<T>
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GroupMapDelegate(IntPtr context, GeometryPtr geometryPtr, ushort devIdx);

        private readonly GroupMapDelegate _f;
        private readonly Controller<T> _controller;
        private readonly PList<int> _keys;
        private readonly PList<DatagramPtr> _datagrams;
        private readonly Dictionary<object, int> _keymap;
        private int _k;

        internal GroupGuard(Func<Device, object?> map, Controller<T> controller)
        {
            _controller = controller;
            _keys = new PList<int>();
            _datagrams = new PList<DatagramPtr>();
            _keymap = new Dictionary<object, int>();
            _k = 0;
            _f = (context, geometryPtr, devIdx) =>
            {
                var key = map(new Device(devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx)));
                return key != null ? _keymap[key] : -1;
            };
        }

        public GroupGuard<T> Set(object key, IDatagram d)
        {
            if (_keymap.ContainsKey(key)) throw new AUTDException("Key already exists");
            _datagrams.Add(d.Ptr(_controller.Geometry));
            _keys.Add(_k);
            _keymap[key] = _k++;
            return this;
        }

        public GroupGuard<T> Set(object key, (IDatagram, IDatagram) d) => Set(key, new DatagramTuple(d));

        public async Task SendAsync()
        {
            await Task.Run(Send);
        }

        public void Send()
        {
            unsafe
            {
                fixed (int* kp = &_keys.Items[0])
                fixed (DatagramPtr* dp = &_datagrams.Items[0])
                    NativeMethodsBase.AUTDControllerGroup(_controller.Ptr, Marshal.GetFunctionPointerForDelegate(_f), new ContextPtr { Item1 = IntPtr.Zero }, _controller.Geometry.Ptr, kp, dp, (ushort)_keys.Count).Validate();
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
