using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;


namespace AUTD3Sharp
{
    /// <summary>
    /// Datagram to synchronize devices
    /// </summary>
    public sealed class Synchronize : IDatagram
    {
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSynchronize();
    }

}
