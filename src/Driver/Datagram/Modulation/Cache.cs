#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    /// <summary>
    /// Modulation to cache the result of calculation
    /// </summary>
    [Modulation(NoCache = true, ConfigNoChange = true, NoTransform = true, NoRadiationPressure = true)]
    public sealed partial class Cache<TM> : IEnumerable<EmitIntensity>
    where TM : AUTD3Sharp.Driver.Datagram.Modulation.IModulation
    {
        private readonly TM _m;
        private EmitIntensity[] _cache;

        public Cache(TM m)
        {
            _m = m;
            _cache = Array.Empty<EmitIntensity>();
            LoopBehavior = m.InternalLoopBehavior();
            _config = m.InternalSamplingConfiguration();
        }

        public ReadOnlySpan<EmitIntensity> Init()
        {
            if (_cache.Length != 0) return Buffer;
            var ptr = NativeMethodsBase.AUTDModulationCalc(_m.ModulationPtr());
            var res = ptr.Validate();
            _cache = new EmitIntensity[ptr.result_len];
            unsafe
            {
                fixed (EmitIntensity* pBuf = &_cache[0])
                    NativeMethodsBase.AUTDModulationCalcGetResult(res, (byte*)pBuf);
            }
            return Buffer;
        }

        private ModulationPtr ModulationPtr()
        {
            Init();
            unsafe
            {
                fixed (EmitIntensity* pBuf = &_cache[0])
                    return NativeMethodsBase.AUTDModulationCustom(_config.Internal, (byte*)pBuf, (ulong)_cache.Length, LoopBehavior.Internal);
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
