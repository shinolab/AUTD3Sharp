using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [Modulation(NoCache = true, ConfigNoChange = true, NoTransform = true, NoRadiationPressure = true)]
    public sealed partial class Cache<TM> : IEnumerable<byte>
    where TM : IModulation
    {
        private readonly TM _m;
        private byte[] _cache;

        public Cache(TM m)
        {
            _m = m;
            _cache = Array.Empty<byte>();
            LoopBehavior = m.LoopBehavior();
            _config = m.SamplingConfig();
        }

        public ReadOnlySpan<byte> Init(Geometry geometry)
        {
            if (_cache.Length != 0) return Buffer;
            var ptr = NativeMethodsBase.AUTDModulationCalc(_m.ModulationPtr(geometry), geometry.Ptr);
            var res = ptr.Validate();
            _cache = new byte[NativeMethodsBase.AUTDModulationCalcGetSize(res)];
            unsafe
            {
                fixed (byte* pBuf = &_cache[0])
                    NativeMethodsBase.AUTDModulationCalcGetResult(res, pBuf);
            }
            NativeMethodsBase.AUTDModulationCalcFreeResult(res);
            return Buffer;
        }

        private ModulationPtr ModulationPtr(Geometry geometry)
        {
            Init(geometry);
            unsafe
            {
                fixed (byte* pBuf = &_cache[0])
                    return NativeMethodsBase.AUTDModulationRaw(_config, LoopBehavior, pBuf, (ushort)_cache.Length);
            }
        }

        public byte this[int index] => _cache[index];

        public ReadOnlySpan<byte> Buffer => new ReadOnlySpan<byte>(_cache);

        public IEnumerator<byte> GetEnumerator()
        {
            foreach (var e in _cache)
                yield return e;
        }

        [ExcludeFromCodeCoverage] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
