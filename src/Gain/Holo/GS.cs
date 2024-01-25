using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Gain to produce multiple foci with GS algorithm
    /// </summary>
    /// <remarks>Asier Marzo and Bruce W Drinkwater. Holographic acoustic tweezers.Proceedings of theNational Academy of Sciences, 116(1):84–89, 2019.</remarks>
    /// <typeparam name="TB">Backend</typeparam>
    public sealed class GS<TB> : Holo<GS<TB>>
        where TB : Backend
    {
        private readonly TB _backend;

        public GS(TB backend) : base(EmissionConstraint.DontCare())
        {
            _backend = backend;
            Repeat = 100;
        }

        /// <summary>
        /// Parameter. See the paper for details.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public GS<TB> WithRepeat(uint value)
        {
            Repeat = value;
            return this;
        }

        public uint Repeat { get; private set; }

        internal override GainPtr GainPtr(Geometry geometry)
        {
            return _backend.Gs(Foci.ToArray(), Amps.ToArray(),
                (ulong)Amps.Count, Repeat, Constraint.Ptr);
        }
    }
}
