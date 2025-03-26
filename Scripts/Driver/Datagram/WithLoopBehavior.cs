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
            internal DatagramPtr WithLoopBehavior(Geometry geometry, Segment segment, TransitionMode? transitionMode, LoopBehavior loopBehavior);
        }
    }

    public sealed class WithLoopBehavior : Driver.Datagram.IDatagram
    {
        public Driver.Datagram.IDatagramL Inner;
        public LoopBehavior LoopBehavior;
        public Segment Segment;
        public TransitionMode? TransitionMode;

        public WithLoopBehavior(Driver.Datagram.IDatagramL inner, LoopBehavior loopBehavior, Segment segment, TransitionMode? transitionMode)
        {
            Inner = inner;
            LoopBehavior = loopBehavior;
            Segment = segment;
            TransitionMode = transitionMode;
        }

        DatagramPtr Driver.Datagram.IDatagram.Ptr(Geometry geometry) => Inner.WithLoopBehavior(geometry, Segment, TransitionMode, LoopBehavior);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
