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
        public EmissionConstraint EmissionConstraint { get; init; } = EmissionConstraint.Clamp(EmitIntensity.Min, EmitIntensity.Max);

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
        public Backend Backend;

        public GS(IEnumerable<(Point3, Amplitude)> foci, GSOption option, Backend backend)
        {
            Foci = foci as (Point3, Amplitude)[] ?? foci.ToArray();
            Option = option;
            Backend = backend;
        }

        GainPtr IGain.GainPtr(Geometry _) => Backend.Gs(Foci, Option.ToNative());
    }
}