#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Geometry;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Gain
{
    public sealed class Group : Driver.Datagram.Gain
    {
        private readonly Func<Device, Transducer, object?> _f;
        private readonly Dictionary<object, Driver.Datagram.Gain> _map;

        public Group(Func<Device, Transducer, object?> f)
        {
            _f = f;
            _map = new Dictionary<object, Driver.Datagram.Gain>();
        }

        public Group Set(object key, Driver.Datagram.Gain gain)
        {
            _map[key] = gain;
            return this;
        }

        internal override GainPtr GainPtr(Geometry geometry)
        {
            var keymap = new Dictionary<object, int>();
            var deviceIndices = geometry.Devices().Select(dev => (uint)dev.Idx).ToArray();
            unsafe
            {
                fixed (uint* deviceIndicesPtr = &deviceIndices[0])
                {
                    var map = NativeMethodsBase.AUTDGainGroupCreateMap(deviceIndicesPtr, (uint)deviceIndices.Length);
                    var k = 0;
                    foreach (var dev in geometry.Devices())
                    {
                        var m = new int[dev.NumTransducers];
                        foreach (var tr in dev)
                        {
                            var key = _f(dev, tr);
                            if (key != null)
                            {
                                if (!keymap.ContainsKey(key)) keymap[key] = k++;
                                m[tr.Idx] = keymap[key];
                            }
                            else
                                m[tr.Idx] = -1;
                        }

                        fixed (int* p = &m[0])
                            map = NativeMethodsBase.AUTDGainGroupMapSet(map, (uint)dev.Idx, p);
                    }

                    var keys = new int[_map.Count];
                    var values = new GainPtr[_map.Count];
                    foreach (var (kv, i) in _map.Select((v, i) => (v, i)))
                    {
                        if (!keymap.ContainsKey(kv.Key)) throw new AUTDException("Unknown group key");
                        keys[i] = keymap[kv.Key];
                        values[i] = kv.Value.GainPtr(geometry);
                    }

                    fixed (int* keysPtr = keys)
                    fixed (GainPtr* valuesPtr = values)
                        return NativeMethodsBase.AUTDGainGroup(
                            map,
                            keysPtr,
                            valuesPtr,
                            (uint)values.Length);
                }
            }
        }
    }

}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
