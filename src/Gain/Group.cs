#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain
{
    [Gain]
    public sealed partial class Group
    {
        private readonly Func<Device, Func<Transducer, object?>> _f;
        private readonly Dictionary<object, Driver.Datagram.Gain.IGain> _map;

        public Group(Func<Device, Func<Transducer, object?>> f)
        {
            _f = f;
            _map = new();
        }

        public Group Set(object key, Driver.Datagram.Gain.IGain gain)
        {
            _map[key] = gain;
            return this;
        }

        private GainPtr GainPtr(Geometry geometry)
        {
            var keymap = new Dictionary<object, int>();
            var deviceIndices = geometry.Devices().Select(dev => (ushort)dev.Idx).ToArray();
            unsafe
            {
                fixed (ushort* deviceIndicesPtr = &deviceIndices[0])
                {
                    var map = NativeMethodsBase.AUTDGainGroupCreateMap(deviceIndicesPtr, (ushort)deviceIndices.Length);
                    var k = 0;
                    foreach (var dev in geometry.Devices())
                    {
                        var m = new int[dev.NumTransducers];
                        foreach (var tr in dev)
                        {
                            var key = _f(dev)(tr);
                            if (key != null)
                            {
                                if (!keymap.ContainsKey(key)) keymap[key] = k++;
                                m[tr.Idx] = keymap[key];
                            }
                            else
                                m[tr.Idx] = -1;
                        }

                        fixed (int* p = &m[0])
                            map = NativeMethodsBase.AUTDGainGroupMapSet(map, (ushort)dev.Idx, p);
                    }

                    var keys = new int[_map.Count];
                    var values = new GainPtr[_map.Count];
                    foreach (var (kv, i) in _map.Select((v, i) => (v, i)))
                    {
                        if (!keymap.TryGetValue(kv.Key, out var value)) throw new AUTDException("Unknown group key");
                        keys[i] = value;
                        values[i] = kv.Value.GainPtr(geometry);
                    }

                    fixed (int* keysPtr = &keys[0])
                    fixed (GainPtr* valuesPtr = &values[0])
                        return NativeMethodsBase.AUTDGainGroup(
                            map,
                            keysPtr,
                            valuesPtr,
                            (uint)values.Length).Validate();
                }
            }
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
