using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Gain to produce multiple foci with greedy algorithm
    /// </summary>
    /// <remarks>
    /// Shun Suzuki, Masahiro Fujiwara, Yasutoshi Makino, and Hiroyuki Shinoda, “Radiation Pressure Field Reconstruction for Ultrasound Midair Haptics by Greedy Algorithm with Brute-Force Search,” in IEEE Transactions on Haptics, doi: 10.1109/TOH.2021.3076489
    /// </remarks>
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
                    return NativeMethodsGainHolo.AUTDGainHoloGreedy(foci, (double*)amps, (ulong)Amps.Count, PhaseDiv, Constraint.Ptr);
                }
            }
        }
    }
}
