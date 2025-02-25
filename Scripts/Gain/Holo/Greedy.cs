using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Gain.Holo
{
    public class GreedyOption
    {
        public byte PhaseDiv { get; init; } = 16;
        public EmissionConstraint EmissionConstraint { get; init; } = EmissionConstraint.Uniform(EmitIntensity.Max);

        internal NativeMethods.GreedyOption ToNative() => new()
        {
            phase_div = PhaseDiv,
            constraint = EmissionConstraint.Inner
        };
    }

    public sealed class Greedy : IGain
    {
        public (Point3, Amplitude)[] Foci;
        public GreedyOption Option;

        public Greedy(IEnumerable<(Point3, Amplitude)> foci, GreedyOption option)
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
                    return NativeMethodsGainHolo.AUTDGainHoloGreedySphere(pPoints, pAmps, (uint)Foci.Length, Option.ToNative());
            }
        }
    }
}