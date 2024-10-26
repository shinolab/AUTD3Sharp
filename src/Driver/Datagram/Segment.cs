using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

namespace AUTD3Sharp
{
    public sealed class SwapSegmentModulation : IDatagram
    {
        private readonly Segment _segment;
        private readonly TransitionModeWrap _transitionMode;

        internal SwapSegmentModulation(Segment segment, TransitionModeWrap transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentModulation(_segment, _transitionMode);
    }

    public sealed class SwapSegmentFociSTM : IDatagram
    {
        private readonly Segment _segment;
        private readonly TransitionModeWrap _transitionMode;

        internal SwapSegmentFociSTM(Segment segment, TransitionModeWrap transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentFociSTM(_segment, _transitionMode);
    }

    public sealed class SwapSegmentGainSTM : IDatagram
    {
        private readonly Segment _segment;
        private readonly TransitionModeWrap _transitionMode;

        internal SwapSegmentGainSTM(Segment segment, TransitionModeWrap transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentGainSTM(_segment, _transitionMode);
    }

    public sealed class SwapSegmentGain : IDatagram
    {
        private readonly Segment _segment;
        private readonly TransitionModeWrap _transitionMode;

        internal SwapSegmentGain(Segment segment, TransitionModeWrap transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentGain(_segment, _transitionMode);
    }

    public static class SwapSegment
    {
        public static SwapSegmentModulation Modulation(Segment segment, TransitionModeWrap transitionMode) => new(segment, transitionMode);
        public static SwapSegmentFociSTM FociSTM(Segment segment, TransitionModeWrap transitionMode) => new(segment, transitionMode);
        public static SwapSegmentGainSTM GainSTM(Segment segment, TransitionModeWrap transitionMode) => new(segment, transitionMode);
        public static SwapSegmentGain Gain(Segment segment, TransitionModeWrap transitionMode) => new(segment, transitionMode);
    }
}
