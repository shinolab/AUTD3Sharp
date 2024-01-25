using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Internal
{
    [ComVisible(false)]
    public interface ILinkBuilder<out T>
    {
        internal LinkBuilderPtr Ptr();
        internal T ResolveLink(LinkPtr ptr);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
