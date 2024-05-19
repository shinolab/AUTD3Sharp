using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class Greedy : Holo<Greedy>
    {
        public Greedy() : base(EmissionConstraint.Uniform(EmitIntensity.Max))
        {
            PhaseDiv = 16;
        }

        [Property]
        public byte PhaseDiv { get; private set; }

        private GainPtr GainPtr(Geometry _)
        {
            unsafe
            {
                fixed (double* foci = Foci.ToArray())
                fixed (Amplitude* amps = Amps.ToArray())
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGreedySphere(foci, (double*)amps, (ulong)Amps.Count, PhaseDiv, Constraint);
                }
            }
        }
    }
}
