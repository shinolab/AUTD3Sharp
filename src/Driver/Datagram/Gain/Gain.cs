using System.Runtime.InteropServices;

using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Gain
{
    [ComVisible(false)]
    public interface IGain
    {
        public GainPtr GainPtr(Geometry geometry);
    }

    public sealed class ChangeGainSegment : IDatagram
    {
        private readonly Segment _segment;

        public ChangeGainSegment(Segment segment)
        {
            _segment = segment;
        }

        DatagramPtr IDatagram.Ptr(Geometry _) => NativeMethodsBase.AUTDDatagramChangeGainSegment(_segment);
    }
}
