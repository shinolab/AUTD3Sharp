using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Cache<TM> : IDisposable
    where TM : IModulation
    {
        private readonly TM _m;
        private ModulationPtr? _ptr;
        private bool _isDisposed;

        public Cache(TM m)
        {
            _m = m;
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
                NativeMethodsBase.AUTDModulationCacheFree(_ptr.Value);
                _ptr = null;
            }

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        private ModulationPtr ModulationPtr()
        {
            if (!_ptr.HasValue)
                _ptr = NativeMethodsBase.AUTDModulationCache(_m
                .ModulationPtr());
            return NativeMethodsBase.AUTDModulationCacheClone(_ptr.Value);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
