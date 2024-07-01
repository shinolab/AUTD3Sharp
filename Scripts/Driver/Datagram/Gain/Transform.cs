using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Driver.Datagram.Gain
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void GainTransformDelegate(ConstPtr context, GeometryPtr geometryPtr, ushort devIdx, byte trIdx, Drive src, NativeMethods.Drive* dst);

    [Gain(NoTransform = true)]
    public sealed partial class Transform<TG>
    where TG : IGain
    {
        private readonly TG _g;
        private readonly GainTransformDelegate _f;

        public Transform(TG g, Func<Device, Func<Transducer, Drive, Drive>> f)
        {
            unsafe
            {
                _g = g;
                _f = (_, geometryPtr, devIdx, trIdx, src, dst) =>
                    {
                        var dev = new Device(devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx));
                        var tr = new Transducer(trIdx, dev.Ptr);
                        var d = f(dev)(tr, src);
                        dst->intensity = d.Intensity.Value;
                        dst->phase = d.Phase.Value;
                    };
            }
        }

        private GainPtr GainPtr(Geometry geometry)
        {
            return NativeMethodsBase.AUTDGainWithTransform(_g.GainPtr(geometry), new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, geometry.Ptr);
        }
    }
}
