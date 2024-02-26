
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain.Holo
{
    /// <summary>
    /// Gain to produce multiple foci with naive linear synthesis
    /// </summary>
    /// <typeparam name="TB">Backend</typeparam>
    [Gain]
    public sealed partial class Naive<TB> : Holo<Naive<TB>>
          where TB : Backend
    {
        private readonly TB _backend;


        public Naive(TB backend) : base(EmissionConstraint.DontCare())
        {
            _backend = backend;
        }

        private GainPtr GainPtr(Geometry _) =>
            _backend.Naive(Foci.ToArray(), Amps.ToArray(),
                (ulong)Amps.Count, Constraint.Ptr);
    }
}
