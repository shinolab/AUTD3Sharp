using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;

namespace AUTD3Sharp.Gain.Holo
{
    public class GSOption
    {
        public uint Repeat { get; init; } = 100;
        public EmissionConstraint EmissionConstraint { get; init; } = EmissionConstraint.Clamp(Intensity.Min, Intensity.Max);

        internal NativeMethods.GSOption ToNative() => new()
        {
            repeat = Repeat,
            constraint = EmissionConstraint.Inner
        };
    }

    public sealed class GS : IGain
    {
        public (Point3, Amplitude)[] Foci;
        public GSOption Option;

        public GS(IEnumerable<(Point3, Amplitude)> foci, GSOption option)
        {
            Foci = foci as (Point3, Amplitude)[] ?? foci.ToArray();
            Option = option;
        }

        GainPtr IGain.GainPtr(Geometry _)
        {
            var points = Foci.Select(f => f.Item1).ToArray();
            var amps = Foci.Select(f => f.Item2.Pascal()).ToArray();
            unsafe
            {
                fixed (Point3* pPoints = &points[0])
                fixed (float* pAmps = &amps[0])
                    return NativeMethodsGainHolo.AUTDGainHoloGSSphere(pPoints, pAmps, (uint)Foci.Length, Option.ToNative());
            }
        }
    }
}