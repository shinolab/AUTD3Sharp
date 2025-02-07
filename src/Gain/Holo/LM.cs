using System;
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
    public class LMOption
    {
        public float Eps1 { get; init; } = 1e-8f;
        public float Eps2 { get; init; } = 1e-8f;
        public float Tau { get; init; } = 1e-3f;
        public uint KMax { get; init; } = 5;
        public float[] Initial { get; init; } = Array.Empty<float>();
        public EmissionConstraint EmissionConstraint { get; init; } = EmissionConstraint.Clamp(EmitIntensity.Min, EmitIntensity.Max);

        internal NativeMethods.LMOption ToNative()
        {
            unsafe
            {
                fixed (float* pInitial = Initial)
                    return new NativeMethods.LMOption
                    {
                        eps_1 = Eps1,
                        eps_2 = Eps2,
                        tau = Tau,
                        k_max = KMax,
                        initial = pInitial,
                        initial_len = (uint)Initial.Length,
                        constraint = EmissionConstraint.Inner
                    };
            }
        }
    }

    public sealed class LM : IGain
    {
        public (Point3, Amplitude)[] Foci;
        public LMOption Option;
        public Backend Backend;

        public LM(IEnumerable<(Point3, Amplitude)> foci, LMOption option, Backend backend)
        {
            Foci = foci as (Point3, Amplitude)[] ?? foci.ToArray();
            Option = option;
            Backend = backend;
        }

        GainPtr IGain.GainPtr(Geometry _) => Backend.Lm(Foci, Option.ToNative());
    }
}