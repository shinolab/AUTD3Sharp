using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{

    [ComVisible(false)]
    public interface IDatagramST<TP>
    {
        public TP RawPtr(Geometry geometry);
        public DatagramPtr IntoSegmentTransition(TP p, Segment segment, TransitionModeWrap? transitionMode);
    }

    public sealed class DatagramWithSegmentTransition<T, TP> : IDatagram
    where T : IDatagramST<TP>
    {
        private readonly T _datagram;
        private readonly Segment _segment;
        private readonly TransitionModeWrap? _transitionMode;

        public DatagramWithSegmentTransition(T datagram, Segment segment, TransitionModeWrap? transitionMode)
        {
            _datagram = datagram;
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => _datagram.IntoSegmentTransition(_datagram.RawPtr(geometry), _segment, _transitionMode);
    }
}
