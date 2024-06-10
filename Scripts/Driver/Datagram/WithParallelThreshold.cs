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

    public static class DatagramWithParallelThresholdExtension
    {
        public static DatagramWithParallelThreshold<T> WithParallelThreshold<T>(this T datagram, ushort threshold) where T : IDatagram => new DatagramWithParallelThreshold<T>(datagram, threshold);
        public static DatagramWithParallelThreshold<DatagramTuple<T1, T2>> WithParallelThreshold<T1, T2>(this (T1, T2) datagram, ushort threshold)
        where T1 : IDatagram
        where T2 : IDatagram
        => new DatagramWithParallelThreshold<DatagramTuple<T1, T2>>(new DatagramTuple<T1, T2>(datagram), threshold);
    }
}
