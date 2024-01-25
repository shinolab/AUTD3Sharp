using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Gain to produce multiple foci with naive linear synthesis
    /// </summary>
    /// <typeparam name="TB">Backend</typeparam>
    public sealed class Naive<TB> : Holo<Naive<TB>>
        where TB : Backend
    {
        private readonly TB _backend;


        public Naive(TB backend) : base(EmissionConstraint.DontCare())
        {
            _backend = backend;
        }

        internal override GainPtr GainPtr(Geometry geometry) =>
            _backend.Naive(Foci.ToArray(), Amps.ToArray(),
                (ulong)Amps.Count, Constraint.Ptr);
    }
}
