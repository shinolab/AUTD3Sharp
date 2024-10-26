using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Cache<TM>
    where TM : IModulation
    {
        private readonly TM _m;
        private ModulationCachePtr? _ptr;

        public Cache(TM m)
        {
            _m = m;
            _ptr = null;
            _loopBehavior = m.LoopBehavior;
        }

        [ExcludeFromCodeCoverage]
        ~Cache()
        {
            if (!_ptr.HasValue) return;
            NativeMethodsBase.AUTDModulationCacheFree(_ptr.Value);
            _ptr = null;
        }

        private ModulationPtr ModulationPtr()
        {
            _ptr ??= NativeMethodsBase.AUTDModulationCache(_m.ModulationPtr());
            return NativeMethodsBase.AUTDModulationCacheClone(_ptr.Value, _loopBehavior);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
