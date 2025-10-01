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
        public interface IDatagramL
        {
            internal DatagramPtr WithFiniteLoop(Geometry geometry, Segment segment, IFiniteTransitionMode transitionMode, ushort loopCount);
        }
    }

    public sealed class WithFiniteLoop : Driver.Datagram.IDatagram
    {
        public Driver.Datagram.IDatagramL Inner;
        public ushort LoopCount;
        public Segment Segment;
        public IFiniteTransitionMode TransitionMode;

        public WithFiniteLoop(Driver.Datagram.IDatagramL inner, ushort loopCount, Segment segment, IFiniteTransitionMode transitionMode)
        {
            Inner = inner;
            LoopCount = loopCount;
            Segment = segment;
            TransitionMode = transitionMode;
        }

        DatagramPtr Driver.Datagram.IDatagram.Ptr(Geometry geometry) => Inner.WithFiniteLoop(geometry, Segment, TransitionMode, LoopCount);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
