using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Gain
{
    public sealed class Cache : IGain
    {
        private readonly IGain _g;
        private GainCachePtr? _ptr;

        public Cache(IGain g)
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

        GainPtr IGain.GainPtr(Geometry geometry)
        {
            _ptr ??= NativeMethodsBase.AUTDGainCache(_g.GainPtr(geometry));
            return NativeMethodsBase.AUTDGainCacheClone(_ptr.Value);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
