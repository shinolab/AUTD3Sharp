using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Driver.Datagram.Gain
{
    [Gain(NoTransform = true)]
    public sealed partial class Transform<TG>
    where TG : IGain
    {
        private readonly TG _g;
        private readonly Func<Device, Transducer, Drive, Drive> _f;

        public Transform(TG g, Func<Device, Transducer, Drive, Drive> f)
        {
            _g = g;
            _f = f;
        }

        private GainPtr GainPtr(Geometry geometry)
        {
            var res = NativeMethodsBase.AUTDGainCalc(_g.GainPtr(geometry), geometry.Ptr).Validate();
            var drives = new Dictionary<int, Drive[]>();
            foreach (var dev in geometry.Devices())
            {
                var d = new Drive[dev.NumTransducers];
                unsafe
                {
                    fixed (Drive* p = &d[0]) NativeMethodsBase.AUTDGainCalcGetResult(res, (NativeMethods.Drive*)p, (uint)dev.Idx);
                }

                foreach (var tr in dev)
                    d[tr.Idx] = _f(dev, tr, d[tr.Idx]);
                drives[dev.Idx] = d;
            }

            NativeMethodsBase.AUTDGainCalcFreeResult(res);
            return geometry.Devices().Aggregate(NativeMethodsBase.AUTDGainRaw(),
                (acc, dev) =>
                {
                    unsafe
                    {
                        fixed (Drive* p = &drives[dev.Idx][0]) return NativeMethodsBase.AUTDGainRawSet(acc, (uint)dev.Idx, (NativeMethods.Drive*)p, (uint)drives[dev.Idx].Length);
                    }
                });
        }
    }
}
