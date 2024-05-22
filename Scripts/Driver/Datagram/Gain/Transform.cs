using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Driver.Datagram.Gain
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void GainTransformDelegate(IntPtr context, GeometryPtr geometryPtr, uint devIdx, byte trIdx, Drive src, NativeMethods.Drive* dst);

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
                _f = (context, geometryPtr, devIdx, trIdx, src, dst) =>
                    {
                        var dev = new Device((int)devIdx, NativeMethodsBase.AUTDDevice(geometryPtr, devIdx));
                        var tr = new Transducer(trIdx, dev.Ptr);
                        var d = f(dev)(tr, src);
                        dst->intensity = d.Intensity.Value;
                        dst->phase = d.Phase.Value;
                    };
            }
        }

        private GainPtr GainPtr(Geometry geometry)
        {
            return NativeMethodsBase.AUTDGainWithTransform(_g.GainPtr(geometry), Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, geometry.Ptr);
        }
    }
}
