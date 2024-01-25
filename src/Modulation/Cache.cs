#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Modulation to cache the result of calculation
    /// </summary>
    public class Cache<TM> : Driver.Datagram.Modulation, IEnumerable<EmitIntensity>
    where TM : Driver.Datagram.Modulation
    {
        private readonly TM _m;
        private EmitIntensity[] _cache;
        private SamplingConfiguration? _samplingConfig;

        public Cache(TM m)
        {
            _m = m;
            _cache = Array.Empty<EmitIntensity>();
            _samplingConfig = null;
        }

        public ReadOnlySpan<EmitIntensity> Calc()
        {
            if (_cache.Length != 0) return Buffer;
            var ptr = NativeMethodsBase.AUTDModulationCalc(_m.ModulationPtr());
            var res = ptr.Validate();
            _cache = new EmitIntensity[ptr.result_len];
            _samplingConfig = SamplingConfiguration.FromFrequencyDivision(ptr.freq_div);
            unsafe
            {
                fixed (EmitIntensity* pBuf = &_cache[0])
                    NativeMethodsBase.AUTDModulationCalcGetResult(res, (byte*)pBuf);
            }
            return Buffer;
        }

        internal override ModulationPtr ModulationPtr()
        {
            Calc();
            unsafe
            {
                fixed (EmitIntensity* pBuf = &_cache[0])
                    return NativeMethodsBase.AUTDModulationCustom(_samplingConfig!.Value.Internal, (byte*)pBuf, (ulong)_cache.Length);

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

    public static class CacheModulationExtensions
    {
        public static Cache<TM> WithCache<TM>(this TM s)
        where TM : Driver.Datagram.Modulation
        {
            return new Cache<TM>(s);
        }
    }
}
