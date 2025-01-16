using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{

    [ComVisible(false)]
    public interface IDatagramS<TP>
    {
        public TP RawPtr(Geometry geometry);
        public DatagramPtr IntoSegmentTransition(TP p, Segment segment, TransitionModeWrap? transitionMode);
    }

    public sealed class DatagramWithSegment<T, TP> : IDatagram
    where T : IDatagramS<TP>
    {
        private readonly T _datagram;
        private readonly Segment _segment;
        private readonly TransitionModeWrap? _transitionMode;

        public DatagramWithSegment(T datagram, Segment segment, TransitionModeWrap? transitionMode)
        {
            _datagram = datagram;
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => _datagram.IntoSegmentTransition(_datagram.RawPtr(geometry), _segment, _transitionMode);
    }
}
