using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Driver.Datagram;

namespace AUTD3Sharp
{
    public sealed class SwapSegmentModulation : IDatagram
    {
        private readonly Segment _segment;
        private readonly ITransitionMode _transitionMode;

        public SwapSegmentModulation(Segment segment, ITransitionMode transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentModulation(_segment.ToNative(), _transitionMode.Inner);
    }

    public sealed class SwapSegmentFociSTM : IDatagram
    {
        private readonly Segment _segment;
        private readonly ITransitionMode _transitionMode;

        public SwapSegmentFociSTM(Segment segment, ITransitionMode transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentFociSTM(_segment.ToNative(), _transitionMode.Inner);
    }

    public sealed class SwapSegmentGainSTM : IDatagram
    {
        private readonly Segment _segment;
        private readonly ITransitionMode _transitionMode;

        public SwapSegmentGainSTM(Segment segment, ITransitionMode transitionMode)
        {
            _segment = segment;
            _transitionMode = transitionMode;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentGainSTM(_segment.ToNative(), _transitionMode.Inner);
    }

    public sealed class SwapSegmentGain : IDatagram
    {
        private readonly Segment _segment;

        public SwapSegmentGain(Segment segment)
        {
            _segment = segment;
        }

        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDDatagramSwapSegmentGain(_segment.ToNative());
    }
}
