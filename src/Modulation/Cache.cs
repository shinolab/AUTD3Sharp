using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Modulation
{
    public sealed class Cache : IModulation
    {
        private readonly IModulation _m;
        private ModulationCachePtr? _ptr;

        public Cache(IModulation m)
        {
            _m = m;
            _ptr = null;
        }

        [ExcludeFromCodeCoverage]
        ~Cache()
        {
            if (!_ptr.HasValue) return;
            NativeMethodsBase.AUTDModulationCacheFree(_ptr.Value);
            _ptr = null;
        }

        ModulationPtr IModulation.ModulationPtr()
        {
            _ptr ??= NativeMethodsBase.AUTDModulationCache(_m.ModulationPtr());
            return NativeMethodsBase.AUTDModulationCacheClone(_ptr.Value);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
