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
    public class NaiveOption
    {
        public EmissionConstraint EmissionConstraint { get; init; } = EmissionConstraint.Clamp(EmitIntensity.Min, EmitIntensity.Max);

        internal NativeMethods.NaiveOption ToNative() => new()
        {
            constraint = EmissionConstraint.Inner
        };
    }

    public sealed class Naive : IGain
    {
        public (Point3, Amplitude)[] Foci;
        public NaiveOption Option;
        public Backend Backend;

        public Naive(IEnumerable<(Point3, Amplitude)> foci, NaiveOption option, Backend backend)
        {
            Foci = foci as (Point3, Amplitude)[] ?? foci.ToArray();
            Option = option;
            Backend = backend;
        }

        GainPtr IGain.GainPtr(Geometry _) => Backend.Naive(Foci, Option.ToNative());
    }
}
