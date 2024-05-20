using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{

    [ComVisible(false)]
    public interface IDatagramS<TP>
    {
        public TP RawPtr(Geometry geometry);
        public DatagramPtr IntoSegment(TP p, Segment segment, bool updateSegment);
    }

    public sealed class DatagramWithSegment<T, TP> : IDatagram
    where T : IDatagramS<TP>
    {
        private readonly T _datagram;
        private readonly Segment _segment;
        private readonly bool _updateSegment;

        public DatagramWithSegment(T datagram, Segment segment, bool updateSegment)
        {
            _datagram = datagram;
            _segment = segment;
            _updateSegment = updateSegment;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry)
        {
            var rawPtr = _datagram.RawPtr(geometry);
            return _datagram.IntoSegment(rawPtr, _segment, _updateSegment);
        }
    }
}
