using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    public sealed class Custom : IGain
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal unsafe delegate void TransducerTestDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx, byte trIdx, NativeMethods.Drive* raw);

        private readonly TransducerTestDelegate _f;

        public Custom(Func<Device, Func<Transducer, Drive>> f)
        {
            unsafe
            {
                _f = (_, geometryPtr, devIdx, trIdx, raw) =>
                {
                    var dev = new Device(devIdx, geometryPtr);
                    *raw = f(dev)(new Transducer(trIdx, devIdx, dev.Ptr)).ToNative();
                };
            }
        }

        GainPtr IGain.GainPtr(Geometry geometry) => NativeMethodsBase.AUTDGainCustom(new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.GeometryPtr);
    }
}
