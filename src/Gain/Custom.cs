using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    [Gain]
    public sealed partial class Custom
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal unsafe delegate void TransducerTestDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx, byte trIdx, Drive* raw);

        private readonly TransducerTestDelegate _f;

        public Custom(Func<Device, Func<Transducer, Drive>> f)
        {
            unsafe
            {
                _f = (_, geometryPtr, devIdx, trIdx, raw) =>
                {
                    var dev = new Device(devIdx, geometryPtr);
                    var tr = new Transducer(trIdx, devIdx, dev.Ptr);
                    *raw = f(dev)(tr);
                };
            }
        }

        private GainPtr GainPtr(Geometry geometry)
        {
            return NativeMethodsBase.AUTDGainCustom(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.Ptr);
        }
    }
}
