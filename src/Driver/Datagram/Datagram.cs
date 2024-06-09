using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public interface IDatagram
    {
        public DatagramPtr Ptr(Geometry geometry);
    }
}
