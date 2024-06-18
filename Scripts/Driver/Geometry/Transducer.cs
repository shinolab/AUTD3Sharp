using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public sealed class Transducer
    {
        internal readonly TransducerPtr Ptr;

        internal Transducer(byte trIdx, DevicePtr ptr)
        {
            Idx = trIdx;
            Ptr = NativeMethodsBase.AUTDTransducer(ptr, trIdx);
        }

        public int Idx { get; }

        public Vector3 Position => NativeMethodsBase.AUTDTransducerPosition(Ptr);
    }
}
