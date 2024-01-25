using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Gain to produce multiple foci with GS-PAT algorithm
    /// </summary>
    /// <remarks>Diego Martinez Plasencia et al. "GS-pat: high-speed multi-point sound-fields for phased arrays of transducers," ACMTrans-actions on Graphics (TOG), 39(4):138–1, 2020.</remarks>
    /// <typeparam name="TB">Backend</typeparam>
    public sealed class GSPAT<TB> : Holo<GSPAT<TB>>
        where TB : Backend
    {
        private readonly TB _backend;

        public GSPAT(TB backend) : base(EmissionConstraint.DontCare())
        {
            _backend = backend;
            Repeat = 100;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GSPAT<TB> WithRepeat(uint value)
        {
            Repeat = value;
            return this;
        }

        public uint Repeat { get; private set; }

        internal override GainPtr GainPtr(Geometry geometry)
        {
            return _backend.Gspat(Foci.ToArray(), Amps.ToArray(), (ulong)Amps.Count, Repeat, Constraint.Ptr);
        }
    }
}
