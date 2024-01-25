#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    /// <summary>
    /// Gain to cache the result of calculation
    /// </summary>
    public sealed class Cache<TG> : Driver.Datagram.Gain
    where TG : Driver.Datagram.Gain
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
                        NativeMethodsBase.AUTDGainCalcGetResult(res, (DriveRaw*)p, (uint)dev.Idx);
                }
                _cache[dev.Idx] = drives;
            }
            NativeMethodsBase.AUTDGainCalcFreeResult(res);
        }

        internal override GainPtr GainPtr(Geometry geometry)
        {
            Init(geometry);
            return geometry.Devices().Aggregate(NativeMethodsBase.AUTDGainCustom(), (acc, dev) =>
            {
                unsafe
                {
                    fixed (Drive* p = &_cache[dev.Idx][0])
                        return NativeMethodsBase.AUTDGainCustomSet(acc, (uint)dev.Idx, (DriveRaw*)p, (uint)_cache[dev.Idx].Length);
                }
            });
        }
    }

    public static class CacheGainExtensions
    {
        public static Cache<TG> WithCache<TG>(this TG s)
        where TG : Driver.Datagram.Gain
        {
            return new Cache<TG>(s);
        }
    }
}
