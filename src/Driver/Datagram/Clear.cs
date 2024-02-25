using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;


namespace AUTD3Sharp
{
    /// <summary>
    /// Datagram for clear all data in devices
    /// </summary>
    public sealed class Clear : IDatagram
    {
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramClear();
    }
}
