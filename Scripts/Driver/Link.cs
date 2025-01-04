using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver
{
    [ComVisible(false)]
    public interface ILinkBuilder<out T>
    {
        public LinkBuilderPtr Ptr();
        public T ResolveLink(RuntimePtr runtime, LinkPtr ptr);
    }
}
