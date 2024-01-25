using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Internal
{
    [ComVisible(false)]
    public interface IDatagram
    {
        internal DatagramPtr Ptr(Geometry geometry);
    }

    internal class NullDatagram : IDatagram
    {
        DatagramPtr IDatagram.Ptr(Geometry geometry)
        {
            return new DatagramPtr { Item1 = IntPtr.Zero };
        }
    }
}
