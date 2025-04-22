using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver
{
    [ComVisible(false)]
    public abstract class Link
    {
        internal LinkPtr Ptr;
        public abstract LinkPtr Resolve();
    }

    [ComVisible(false)]
    public interface ILink
    {
        public void Resolve(LinkPtr ptr);
    }
}
