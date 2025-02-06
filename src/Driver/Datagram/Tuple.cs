using AUTD3Sharp.NativeMethods;
using System.Runtime.InteropServices;

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public class DatagramTuple<TD1, TD2> : IDatagram
        where TD1 : IDatagram
        where TD2 : IDatagram
    {
        internal DatagramTuple((IDatagram, IDatagram) d)
        {
            _d1 = d.Item1;
            _d2 = d.Item2;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramTuple(_d1.Ptr(geometry), _d2.Ptr(geometry));

        private readonly IDatagram _d1;
        private readonly IDatagram _d2;
    }
}
