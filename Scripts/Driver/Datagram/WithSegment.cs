using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    namespace Driver.Datagram
    {
        [ComVisible(false)]
        public interface IDatagramS
        {
            internal DatagramPtr WithSegmentTransition(Geometry geometry, Segment segment, IInfiniteTransitionMode transitionMode);
        }
    }

    public sealed class WithSegment : Driver.Datagram.IDatagram
    {
        public Driver.Datagram.IDatagramS Inner;
        public Segment Segment;
        public IInfiniteTransitionMode TransitionMode;

        public WithSegment(Driver.Datagram.IDatagramS inner, Segment segment, IInfiniteTransitionMode transitionMode)
        {
            Inner = inner;
            Segment = segment;
            TransitionMode = transitionMode;
        }

        DatagramPtr Driver.Datagram.IDatagram.Ptr(Geometry geometry) => Inner.WithSegmentTransition(geometry, Segment, TransitionMode);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
