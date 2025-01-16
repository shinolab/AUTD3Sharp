using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{
    public sealed class DatagramWithTimeout<T> : IDatagram
    where T : IDatagram
    {
        private readonly T _datagram;
        private readonly Duration? _timeout;

        public DatagramWithTimeout(T datagram, Duration? timeout)
        {
            _datagram = datagram;
            _timeout = timeout;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramWithTimeout(_datagram.Ptr(geometry), _timeout.Into());
    }
}

namespace AUTD3Sharp
{
    public static class DatagramWithTimeoutExtension
    {
        public static Driver.Datagram.DatagramWithTimeout<T> WithTimeout<T>(this T datagram, Duration? timeout) where T : Driver.Datagram.IDatagram => new(datagram, timeout);
        public static Driver.Datagram.DatagramWithTimeout<Driver.Datagram.DatagramTuple<T1, T2>> WithTimeout<T1, T2>(this (T1, T2) datagram, Duration? timeout)
        where T1 : Driver.Datagram.IDatagram
        where T2 : Driver.Datagram.IDatagram
        => new(new Driver.Datagram.DatagramTuple<T1, T2>(datagram), timeout);
    }
}
