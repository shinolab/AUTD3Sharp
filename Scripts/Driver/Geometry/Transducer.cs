using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public sealed class Transducer
    {
        internal readonly TransducerPtr Ptr;
        private readonly byte _idx;
        private readonly ushort _devIdx;

        internal Transducer(byte trIdx, ushort devIdx, DevicePtr ptr)
        {
            _idx = trIdx;
            _devIdx = devIdx;
            Ptr = NativeMethodsBase.AUTDTransducer(ptr, trIdx);
        }

        public int Idx() => _idx;
        public int DevIdx() => _devIdx;

        public Point3 Position() => NativeMethodsBase.AUTDTransducerPosition(Ptr);
    }
}
