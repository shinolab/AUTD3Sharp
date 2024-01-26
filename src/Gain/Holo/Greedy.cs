#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Gain to produce multiple foci with greedy algorithm
    /// </summary>
    /// <remarks>
    /// Shun Suzuki, Masahiro Fujiwara, Yasutoshi Makino, and Hiroyuki Shinoda, “Radiation Pressure Field Reconstruction for Ultrasound Midair Haptics by Greedy Algorithm with Brute-Force Search,” in IEEE Transactions on Haptics, doi: 10.1109/TOH.2021.3076489
    /// </remarks>
    public sealed class Greedy : Holo<Greedy>
    {
        public Greedy() : base(EmissionConstraint.Uniform(EmitIntensity.Max))
        {
            PhaseDiv = 16;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Greedy WithPhaseDiv(byte value)
        {
            PhaseDiv = value;
            return this;
        }

        public byte PhaseDiv { get; private set; }

        internal override GainPtr GainPtr(Geometry geometry)
        {
            unsafe
            {
                fixed (float_t* foci = Foci.ToArray())
                fixed (Amplitude* amps = Amps.ToArray())
                {
                    return NativeMethodsGainHolo.AUTDGainHoloGreedy(foci, (float_t*)amps, (ulong)Amps.Count, PhaseDiv, Constraint.Ptr);
                }
            }
        }
    }
}
