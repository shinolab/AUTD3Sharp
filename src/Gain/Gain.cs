#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    public abstract class Gain : Driver.Datagram.Gain
    {
        internal override GainPtr GainPtr(Geometry geometry)
        {
            return Calc(geometry).Aggregate(NativeMethodsBase.AUTDGainCustom(), (acc, d) =>
            {
                unsafe
                {
                    fixed (Drive* p = &d.Value[0])
                        return NativeMethodsBase.AUTDGainCustomSet(acc, (uint)d.Key, (DriveRaw*)p, (uint)d.Value.Length);
                }
            });
        }

        public abstract Dictionary<int, Drive[]> Calc(Geometry geometry);

        public static Dictionary<int, Drive[]> Transform(Geometry geometry, Func<Device, Transducer, Drive> f)
        {
            return geometry.Devices().Select(dev => (dev.Idx, dev.Select(tr => f(dev, tr)).ToArray())).ToDictionary(x => x.Idx, x => x.Item2);
        }
    }
}
