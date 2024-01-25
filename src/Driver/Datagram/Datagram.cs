using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public interface IDatagram
    {
        internal DatagramPtr Ptr(Geometry.Geometry geometry);
    }

    internal class NullDatagram : IDatagram
    {
        DatagramPtr IDatagram.Ptr(Geometry.Geometry _)
        {
            return new DatagramPtr { Item1 = IntPtr.Zero };
        }
    }
}
