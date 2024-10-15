using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public sealed class Transducer
    {
        internal readonly TransducerPtr Ptr;

        internal Transducer(byte trIdx, ushort devIdx, DevicePtr ptr)
        {
            Idx = trIdx;
            DevIdx = devIdx;
            Ptr = NativeMethodsBase.AUTDTransducer(ptr, trIdx);
        }

        public int Idx { get; }

        public int DevIdx { get; }

        public Vector3 Position => NativeMethodsBase.AUTDTransducerPosition(Ptr);
    }
}
