#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GroupMapDelegate(IntPtr context, GeometryPtr geometryPtr, ushort devIdx);

    public sealed class GroupGuard<T>
    {
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
            _f = (_, geometryPtr, devIdx) =>
            {
                var key = map(new Device(devIdx, geometryPtr));
                return key != null ? _keymap[key] : -1;
            };
        }

        public GroupGuard<T> Set<TD>(object key, TD d)
        where TD : IDatagram
        {
            if (_keymap.ContainsKey(key)) throw new AUTDException("Key already exists");
            _datagrams.Add(d.Ptr(_controller));
            _keys.Add(_k);
            _keymap[key] = _k++;
            return this;
        }

        public GroupGuard<T> Set<TD1, TD2>(object key, (TD1, TD2) d)
        where TD1 : IDatagram
        where TD2 : IDatagram
         => Set(key, new DatagramTuple<TD1, TD2>(d));

        public void Send()
        {
            unsafe
            {
                fixed (int* kp = &_keys.Items[0])
                fixed (DatagramPtr* dp = &_datagrams.Items[0])
                {
                    var result = NativeMethodsBase.AUTDControllerGroup(_controller.Ptr,
                        new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero },
                        _controller.GeometryPtr, kp, dp, (ushort)_keys.Count);
                    result.Validate();
                }
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
