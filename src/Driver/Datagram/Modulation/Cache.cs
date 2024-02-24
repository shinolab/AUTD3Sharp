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
    public sealed partial class Cache<TM> : Modulation, IEnumerable<EmitIntensity>
    where TM : Modulation
    {
        private readonly TM _m;
        private EmitIntensity[] _cache;

        public Cache(TM m)
        {
            _m = m;
            _cache = Array.Empty<EmitIntensity>();
            LoopBehavior = m.LoopBehavior;
            Config = m.Config;
        }

        public ReadOnlySpan<EmitIntensity> Calc()
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

        internal override ModulationPtr ModulationPtr()
        {
            Calc();
            unsafe
            {
                fixed (EmitIntensity* pBuf = &_cache[0])
                    return NativeMethodsBase.AUTDModulationCustom(Config.Internal, (byte*)pBuf, (ulong)_cache.Length, LoopBehavior.Internal);
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
