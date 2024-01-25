using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public abstract class Gain : IDatagram
    {
        DatagramPtr IDatagram.Ptr(Geometry.Geometry geometry) => NativeMethodsBase.AUTDGainIntoDatagram(GainPtr(geometry));

        internal abstract GainPtr GainPtr(Geometry.Geometry geometry);
    }
}
