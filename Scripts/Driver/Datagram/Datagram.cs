using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public interface IDatagram
    {
        internal DatagramPtr Ptr(Geometry geometry);
    }
}
