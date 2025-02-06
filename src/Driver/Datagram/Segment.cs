using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

namespace AUTD3Sharp
{
    public sealed class SwapSegmentModulation : IDatagram
    {
        private readonly Segment _segment;
        private readonly TransitionMode _transitionMode;

        internal SwapSegmentModulation(Segment segment, TransitionMode transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentModulation(_segment.ToNative(), _transitionMode.Inner);
    }

    public sealed class SwapSegmentFociSTM : IDatagram
    {
        private readonly Segment _segment;
        private readonly TransitionMode _transitionMode;

        internal SwapSegmentFociSTM(Segment segment, TransitionMode transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentFociSTM(_segment.ToNative(), _transitionMode.Inner);
    }

    public sealed class SwapSegmentGainSTM : IDatagram
    {
        private readonly Segment _segment;
        private readonly TransitionMode _transitionMode;

        internal SwapSegmentGainSTM(Segment segment, TransitionMode transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentGainSTM(_segment.ToNative(), _transitionMode.Inner);
    }

    public sealed class SwapSegmentGain : IDatagram
    {
        private readonly Segment _segment;
        private readonly TransitionMode _transitionMode;

        internal SwapSegmentGain(Segment segment, TransitionMode transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentGain(_segment.ToNative(), _transitionMode.Inner);
    }

    public static class SwapSegment
    {
        public static SwapSegmentModulation Modulation(Segment segment, TransitionMode transitionMode) => new(segment, transitionMode);
        public static SwapSegmentFociSTM FociSTM(Segment segment, TransitionMode transitionMode) => new(segment, transitionMode);
        public static SwapSegmentGainSTM GainSTM(Segment segment, TransitionMode transitionMode) => new(segment, transitionMode);
        public static SwapSegmentGain Gain(Segment segment, TransitionMode transitionMode) => new(segment, transitionMode);
    }
}
