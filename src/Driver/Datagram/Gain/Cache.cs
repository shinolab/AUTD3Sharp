using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Driver.Datagram.Gain
{
    [Gain]
    public sealed partial class Cache<TG>
    where TG : IGain
    {
        private readonly TG _g;
        private readonly Dictionary<int, Drive[]> _cache;

        public Cache(TG g)
        {
            _g = g;
            _cache = new Dictionary<int, Drive[]>();
        }

        public ReadOnlyDictionary<int, Drive[]> Drives()
        {
            return new ReadOnlyDictionary<int, Drive[]>(_cache);
        }

        private void Init(Geometry geometry)
        {
            var deviceIndices = geometry.Devices().Select(d => d.Idx).ToArray();
            if (_cache.Count == deviceIndices.Length && deviceIndices.All(i => _cache.ContainsKey(i))) return;
            var res = NativeMethodsBase.AUTDGainCalc(_g.GainPtr(geometry), geometry.Ptr).Validate();
            foreach (var dev in geometry.Devices())
            {
                var drives = new Drive[dev.NumTransducers];
                unsafe
                {
                    fixed (Drive* p = &drives[0])
                        NativeMethodsBase.AUTDGainCalcGetResult(res, (NativeMethods.Drive*)p, dev.Ptr);
                }
                _cache[dev.Idx] = drives;
            }
            NativeMethodsBase.AUTDGainCalcFreeResult(res);
        }

        private GainPtr GainPtr(Geometry geometry)
        {
            Init(geometry);
            return geometry.Devices().Aggregate(NativeMethodsBase.AUTDGainRaw(), (acc, dev) =>
            {
                unsafe
                {
                    fixed (Drive* p = &_cache[dev.Idx][0])
                        return NativeMethodsBase.AUTDGainRawSet(acc, (ushort)dev.Idx, (NativeMethods.Drive*)p, (byte)_cache[dev.Idx].Length);
                }
            });
        }
    }
}
