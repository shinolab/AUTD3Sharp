using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver
{
    [ComVisible(false)]
    public interface ILinkBuilder<out T>
    {
        internal LinkBuilderPtr Ptr();
        internal T ResolveLink(LinkPtr ptr);
    }
}
