using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [Modulation(NoCache = true, ConfigNoChange = true, NoTransform = true, NoRadiationPressure = true)]
    public sealed partial class Cache<TM> : IEnumerable<EmitIntensity>
    where TM : IModulation
    {
        private readonly TM _m;
        private EmitIntensity[] _cache;

        public Cache(TM m)
        {
            _m = m;
            _cache = Array.Empty<EmitIntensity>();
            this.LoopBehavior = m.LoopBehavior();
            _config = m.SamplingConfig();
        }

        public ReadOnlySpan<EmitIntensity> Init(Geometry geometry)
        {
            if (_cache.Length != 0) return Buffer;
            var ptr = NativeMethodsBase.AUTDModulationCalc(_m.ModulationPtr(geometry), geometry.Ptr);
            var res = ptr.Validate();
            _cache = new EmitIntensity[NativeMethodsBase.AUTDModulationCalcGetSize(res)];
            unsafe
            {
                fixed (EmitIntensity* pBuf = &_cache[0])
                    NativeMethodsBase.AUTDModulationCalcGetResult(res, (byte*)pBuf);
            }
            NativeMethodsBase.AUTDModulationCalcFreeResult(res);
            return Buffer;
        }

        private ModulationPtr ModulationPtr(Geometry geometry)
        {
            Init(geometry);
            unsafe
            {
                fixed (EmitIntensity* pBuf = &_cache[0])
                    return NativeMethodsBase.AUTDModulationRaw(_config, LoopBehavior, (byte*)pBuf, (uint)_cache.Length);
            }
        }

        public EmitIntensity this[int index] => _cache[index];

        public ReadOnlySpan<EmitIntensity> Buffer => new ReadOnlySpan<EmitIntensity>(_cache);

        public IEnumerator<EmitIntensity> GetEnumerator()
        {
            foreach (var e in _cache)
                yield return e;
        }

        [ExcludeFromCodeCoverage] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
