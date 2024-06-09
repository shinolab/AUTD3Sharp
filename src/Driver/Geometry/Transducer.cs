using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public sealed class Transducer
    {
        internal readonly TransducerPtr Ptr;

        internal Transducer(byte trIdx, DevicePtr ptr)
        {
            Idx = (int)trIdx;
            Ptr = NativeMethodsBase.AUTDTransducer(ptr, trIdx);
        }

        public int Idx { get; }

        public Vector3 Position
        {
            get
            {
                unsafe
                {
                    var pos = stackalloc float[3];
                    NativeMethodsBase.AUTDTransducerPosition(Ptr, pos);
                    return new Vector3(pos[0], pos[1], pos[2]);
                }
            }
        }
    }
}
