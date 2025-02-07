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
    public class GSPATOption
    {
        public uint Repeat { get; init; } = 100;
        public EmissionConstraint EmissionConstraint { get; init; } = EmissionConstraint.Clamp(EmitIntensity.Min, EmitIntensity.Max);

        internal NativeMethods.GSPATOption ToNative() => new()
        {
            repeat = Repeat,
            constraint = EmissionConstraint.Inner
        };
    }

    public sealed class GSPAT : IGain
    {
        public (Point3, Amplitude)[] Foci;
        public GSPATOption Option;
        public Backend Backend;

        public GSPAT(IEnumerable<(Point3, Amplitude)> foci, GSPATOption option, Backend backend)
        {
            Foci = foci as (Point3, Amplitude)[] ?? foci.ToArray();
            Option = option;
            Backend = backend;
        }

        GainPtr IGain.GainPtr(Geometry _) => Backend.Gspat(Foci, Option.ToNative());
    }
}