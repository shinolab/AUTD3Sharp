using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;
using AUTD3Sharp.Derive;
using System.Collections.Generic;
using System.Linq;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class Greedy : Holo<Greedy>
    {
        public Greedy(IEnumerable<(Vector3, Amplitude)> iter) : base(EmissionConstraint.Uniform(EmitIntensity.Max), iter)
        {
            PhaseDiv = 16;
        }

        [Property]
        public byte PhaseDiv { get; private set; }

        private GainPtr GainPtr(Geometry _)
        {
            unsafe
            {
                fixed (float* foci = &Foci[0])
                fixed (Amplitude* amps = &Amps[0])
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGreedySphere(foci, (float*)amps, (uint)Amps.Length, PhaseDiv, Constraint);
                }
            }
        }
    }
}
