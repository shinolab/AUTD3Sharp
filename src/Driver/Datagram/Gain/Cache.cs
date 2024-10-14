using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Driver.Datagram.Gain
{
    [Gain]
    public sealed partial class Cache<TG> : IDisposable
    where TG : IGain
    {
        private readonly TG _g;
        private GainPtr? _ptr;
        private bool _isDisposed;

        public Cache(TG g)
        {
            _g = g;
            _ptr = null;
            _isDisposed = false;
        }

        ~Cache()
        {
            Dispose();

        }

        public void Dispose()
        {
            if (_isDisposed) return;

            if (_ptr.HasValue)
            {
                NativeMethodsBase.AUTDGainCacheFree(_ptr.Value);
                _ptr = null;
            }

            _isDisposed = true;
            GC.SuppressFinalize(this);

        }

        private GainPtr GainPtr(Geometry geometry)
        {
            if (!_ptr.HasValue)
                _ptr = NativeMethodsBase.AUTDGainCache(_g.GainPtr(geometry));
            return NativeMethodsBase.AUTDGainCacheClone(_ptr.Value);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
