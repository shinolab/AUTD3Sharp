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
        private SamplingConfigWrap? _sampling_config;

        public Cache(TM m)
        {
            _m = m;
            _cache = Array.Empty<byte>();
            _loopBehavior = m.LoopBehavior;
        }

        public ReadOnlySpan<byte> Init()
        {
            if (_cache.Length != 0) return Buffer;
            var ptr = NativeMethodsBase.AUTDModulationCalc(_m.ModulationPtr());
            var res = ptr.Validate();
            _sampling_config = ptr.config;
            _cache = new byte[NativeMethodsBase.AUTDModulationCalcGetSize(res)];
            unsafe
            {
                fixed (byte* pBuf = &_cache[0])
                    NativeMethodsBase.AUTDModulationCalcGetResult(res, pBuf);
            }
            NativeMethodsBase.AUTDModulationCalcFreeResult(res);
            return Buffer;
        }

        private ModulationPtr ModulationPtr()
        {
            Init();
            unsafe
            {
                fixed (byte* pBuf = &_cache[0])
                    return NativeMethodsBase.AUTDModulationRaw(_sampling_config!.Value, LoopBehavior, pBuf, (ushort)_cache.Length);
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
