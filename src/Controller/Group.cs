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
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int GroupMapDelegate(IntPtr context, GeometryPtr geometryPtr, ushort devIdx);

    public class GroupDictionary : Dictionary<object, IDatagram>
    {
        public void Add<TD1, TD2>(object key, (TD1, TD2) d) where TD1 : IDatagram where TD2 : IDatagram => Add(key, new DatagramTuple<TD1, TD2>(d));
    }

    public partial class Sender
    {
        public void GroupSend(Func<Device, object?> keyMap, GroupDictionary datagramMap)
        {
            var keys = new PList<int>();
            var keymap = new Dictionary<object, int>();
            var datagrams = new PList<DatagramPtr>();
            var k = 0;

            foreach (var (key, d) in datagramMap)
            {
                datagrams.Add(d.Ptr(Geometry));
                keys.Add(k);
                keymap[key] = k++;
            }

            unsafe
            {
                fixed (int* kp = &keys.Items[0])
                fixed (DatagramPtr* dp = &datagrams.Items[0])
                {
                    NativeMethodsBase.AUTDControllerGroup(Ptr,
                       new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate((GroupMapDelegate)F) }, new ConstPtr { Item1 = IntPtr.Zero },
                       Geometry.GeometryPtr, kp, dp, (ushort)keys.Count).Validate();
                }
            }

            return;

            int F(IntPtr _, GeometryPtr geometryPtr, ushort devIdx)
            {
                var key = keyMap(new Device(devIdx, geometryPtr));
                return key is null ? -1 : keymap[key];
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
