using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Gain
{
    public sealed class Group : IGain
    {
        public Func<Device, Func<Transducer, object?>> KeyMap;
        public Dictionary<object, IGain> GainMap;

        public Group(Func<Device, Func<Transducer, object?>> keyMap, Dictionary<object, IGain> gainMap)
        {
            KeyMap = keyMap;
            GainMap = gainMap;
        }

        GainPtr IGain.GainPtr(Geometry geometry)
        {
            var keymap = new Dictionary<object, int>();
            var deviceIndices = geometry.Devices().Select(dev => (ushort)dev.Idx()).ToArray();
            unsafe
            {
                fixed (ushort* deviceIndicesPtr = &deviceIndices[0])
                {
                    var map = NativeMethodsBase.AUTDGainGroupCreateMap(deviceIndicesPtr, (ushort)deviceIndices.Length);
                    var k = 0;
                    foreach (var dev in geometry.Devices())
                    {
                        var m = new int[dev.NumTransducers()];
                        foreach (var tr in dev)
                        {
                            var key = KeyMap(dev)(tr);
                            if (key != null)
                            {
                                if (!keymap.ContainsKey(key)) keymap[key] = k++;
                                m[tr.Idx()] = keymap[key];
                            }
                            else
                                m[tr.Idx()] = -1;
                        }

                        fixed (int* p = &m[0]) map = NativeMethodsBase.AUTDGainGroupMapSet(map, (ushort)dev.Idx(), p);
                    }

                    var keys = new int[GainMap.Count];
                    var values = new GainPtr[GainMap.Count];
                    foreach (var (kv, i) in GainMap.Select((v, i) => (v, i)))
                    {
                        if (!keymap.TryGetValue(kv.Key, out var value)) throw new AUTDException("Unknown group key");
                        keys[i] = value;
                        values[i] = kv.Value.GainPtr(geometry);
                    }

                    fixed (int* keysPtr = &keys[0])
                    fixed (GainPtr* valuesPtr = &values[0])
                        return NativeMethodsBase.AUTDGainGroup(map, keysPtr, valuesPtr, (uint)values.Length);
                }
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
