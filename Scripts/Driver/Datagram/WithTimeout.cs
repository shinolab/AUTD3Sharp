using AUTD3Sharp.NativeMethods;
using System;

namespace AUTD3Sharp.Driver.Datagram
{
    public sealed class DatagramWithTimeout<T> : IDatagram
    where T : IDatagram
    {
        private readonly T _datagram;
        private readonly TimeSpan _timeout;

        public DatagramWithTimeout(T datagram, TimeSpan timeout)
        {
            _datagram = datagram;
            _timeout = timeout;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramWithTimeout(_datagram.Ptr(geometry), (ulong)(_timeout.TotalMilliseconds * 1000 * 1000));
    }
}

namespace AUTD3Sharp
{
    public static class DatagramWithTimeoutExtension
    {
        public static Driver.Datagram.DatagramWithTimeout<T> WithTimeout<T>(this T datagram, TimeSpan timeout) where T : Driver.Datagram.IDatagram => new(datagram, timeout);
        public static Driver.Datagram.DatagramWithTimeout<Driver.Datagram.DatagramTuple<T1, T2>> WithTimeout<T1, T2>(this (T1, T2) datagram, TimeSpan timeout)
        where T1 : Driver.Datagram.IDatagram
        where T2 : Driver.Datagram.IDatagram
        => new(new Driver.Datagram.DatagramTuple<T1, T2>(datagram), timeout);
    }
}
