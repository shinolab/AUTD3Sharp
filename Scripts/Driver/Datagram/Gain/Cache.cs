using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Driver.Datagram.Gain
{
    [Gain]
    public sealed partial class Cache<TG>
    where TG : IGain
    {
        private readonly TG _g;
        private GainCachePtr? _ptr;

        public Cache(TG g)
        {
            _g = g;
            _ptr = null;
        }

        [ExcludeFromCodeCoverage]
        ~Cache()
        {
            if (!_ptr.HasValue) return;
            NativeMethodsBase.AUTDGainCacheFree(_ptr.Value);
            _ptr = null;
        }

        private GainPtr GainPtr(Geometry geometry)
        {
            _ptr ??= NativeMethodsBase.AUTDGainCache(_g.GainPtr(geometry));
            return NativeMethodsBase.AUTDGainCacheClone(_ptr.Value);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
