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
                fixed (float* foci = Foci.ToArray())
                fixed (Amplitude* amps = Amps.ToArray())
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGreedySphere(foci, (float*)amps, (uint)Amps.Count, PhaseDiv, Constraint);
                }
            }
        }
    }
}
