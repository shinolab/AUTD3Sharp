using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public class GroupDictionary : Dictionary<object, IDatagram>
    {
        public void Add<TD1, TD2>(object key, (TD1, TD2) d) where TD1 : IDatagram where TD2 : IDatagram => Add(key, new DatagramTuple<TD1, TD2>(d));
    }

    public sealed class Group : IDatagram
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int GroupMapDelegate(IntPtr context, GeometryPtr geometryPtr, ushort devIdx);

        private readonly Func<Device, object?> _keyMap;
        private readonly GroupDictionary _datagramMap;

        public Group(Func<Device, object?> keyMap, GroupDictionary datagramMap)
        {
            _keyMap = keyMap;
            _datagramMap = datagramMap;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry)
        {
            var keys = new PList<int>();
            var keymap = new Dictionary<object, int>();
            var datagrams = new PList<DatagramPtr>();
            var k = 0;

            foreach (var (key, d) in _datagramMap)
            {
                datagrams.Add(d.Ptr(geometry));
                keys.Add(k);
                keymap[key] = k++;
            }

            unsafe
            {
                fixed (int* kp = &keys.Items[0])
                fixed (DatagramPtr* dp = &datagrams.Items[0])
                {
                    return NativeMethodsBase.AUTDDatagramGroup(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate((GroupMapDelegate)F) }, new ConstPtr { Item1 = IntPtr.Zero },
                       geometry.GeometryPtr, kp, dp, (ushort)keys.Count);
                }
            }

            int F(IntPtr _, GeometryPtr geometryPtr, ushort devIdx)
            {
                var key = _keyMap(new Device(devIdx, geometryPtr));
                return key is null ? -1 : keymap[key];
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
