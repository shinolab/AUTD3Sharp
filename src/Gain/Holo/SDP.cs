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
    /// Gain to produce multiple foci by solving Semi-Definite Programming
    /// </summary>
    /// <remarks>Inoue, Seki, Yasutoshi Makino, and Hiroyuki Shinoda. "Active touch perception produced by airborne ultrasonic haptic hologram." 2015 IEEE World Haptics Conference (WHC). IEEE, 2015.</remarks>
    /// <typeparam name="TB">Backend</typeparam>
    public sealed class SDP<TB> : Holo<SDP<TB>>
        where TB : Backend
    {
        private readonly TB _backend;

        public SDP(TB backend) : base(EmissionConstraint.DontCare())
        {
            _backend = backend;
            Alpha = (float_t)1e-3;
            Lambda = (float_t)0.9;
            Repeat = 100;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SDP<TB> WithAlpha(float_t value)
        {
            Alpha = value;
            return this;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SDP<TB> WithLambda(float_t value)
        {
            Lambda = value;
            return this;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SDP<TB> WithRepeat(uint value)
        {
            Repeat = value;
            return this;
        }


        public float_t Alpha { get; private set; }

        public float_t Lambda { get; private set; }

        public uint Repeat { get; private set; }

        internal override GainPtr GainPtr(Geometry geometry) =>
            _backend.Sdp(Foci.ToArray(), Amps.ToArray(),
                (ulong)Amps.Count, Alpha, Repeat, Lambda, Constraint.Ptr);
    }
}
