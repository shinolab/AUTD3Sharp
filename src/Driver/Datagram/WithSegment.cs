using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public interface IDatagramS
    {
        internal DatagramPtr WithSegmentTransition(Geometry geometry, Segment segment, TransitionMode? transitionMode);
    }

    public sealed class WithSegment : IDatagram
    {
        public IDatagramS Inner;
        public Segment Segment;
        public TransitionMode? TransitionMode;

        public WithSegment(IDatagramS inner, Segment segment, TransitionMode? transitionMode)
        {
            Inner = inner;
            Segment = segment;
            TransitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => Inner.WithSegmentTransition(geometry, Segment, TransitionMode);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
