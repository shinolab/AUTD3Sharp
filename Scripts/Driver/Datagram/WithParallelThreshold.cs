using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{
    public sealed class DatagramWithParallelThreshold<T> : IDatagram
    where T : IDatagram
    {
        private readonly T _datagram;
        private readonly ushort _threshold;

        public DatagramWithParallelThreshold(T datagram, ushort threshold)
        {
            _datagram = datagram;
            _threshold = threshold;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramWithParallelThreshold(_datagram.Ptr(geometry), _threshold);
    }
}

namespace AUTD3Sharp
{
    public static class DatagramWithParallelThresholdExtension
    {
        public static Driver.Datagram.DatagramWithParallelThreshold<T> WithParallelThreshold<T>(this T datagram, ushort threshold) where T : Driver.Datagram.IDatagram => new(datagram, threshold);
        public static Driver.Datagram.DatagramWithParallelThreshold<Driver.Datagram.DatagramTuple<T1, T2>> WithParallelThreshold<T1, T2>(this (T1, T2) datagram, ushort threshold)
        where T1 : Driver.Datagram.IDatagram
        where T2 : Driver.Datagram.IDatagram
        => new(new Driver.Datagram.DatagramTuple<T1, T2>(datagram), threshold);
    }
}
