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

    public static class DatagramWithTimeoutExtension
    {
        public static DatagramWithTimeout<T> WithTimeout<T>(this T datagram, TimeSpan timeout) where T : IDatagram => new DatagramWithTimeout<T>(datagram, timeout);
        public static DatagramWithTimeout<DatagramTuple<T1, T2>> WithTimeout<T1, T2>(this (T1, T2) datagram, TimeSpan timeout)
        where T1 : IDatagram
        where T2 : IDatagram
        => new DatagramWithTimeout<DatagramTuple<T1, T2>>(new DatagramTuple<T1, T2>(datagram), timeout);
    }
}
