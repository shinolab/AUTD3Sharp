using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver
{
    [ComVisible(false)]
    public abstract class Link
    {
        internal LinkPtr Ptr;
        internal abstract LinkPtr Resolve();
    }
}
