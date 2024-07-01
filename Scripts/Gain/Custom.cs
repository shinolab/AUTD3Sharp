using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    [Gain]
    public sealed partial class Custom
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal unsafe delegate void TransducerTestDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx, byte trIdx, NativeMethods.Drive* raw);

        private readonly TransducerTestDelegate _f;

        public Custom(Func<Device, Func<Transducer, Drive>> f)
        {
            unsafe
            {
                _f = (_, geometryPtr, devIdx, trIdx, raw) =>
                {
                    var dev = new Device(devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx));
                    var tr = new Transducer(trIdx, dev.Ptr);
                    var d = f(dev)(tr);
                    raw->phase = d.Phase.Value;
                    raw->intensity = d.Intensity.Value;
                };
            }
        }

        private GainPtr GainPtr(Geometry geometry)
        {
            return NativeMethodsBase.AUTDGainCustom(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.Ptr);
        }
    }
}
