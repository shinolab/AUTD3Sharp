using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Internal
{
    [ComVisible(false)]
    public abstract class Gain : IDatagram
    {
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDGainIntoDatagram(GainPtr(geometry));

        internal abstract GainPtr GainPtr(Geometry geometry);
    }
}
