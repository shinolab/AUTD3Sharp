using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;
using System.Collections.Generic;
using System.Linq;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    public sealed partial class Naive<TB> : Holo<Naive<TB>>
          where TB : Backend
    {
        private readonly TB _backend;


        public Naive(TB backend, IEnumerable<(Vector3, Amplitude)> iter) : base(EmissionConstraint.DontCare, iter)
        {
            _backend = backend;
        }

        private GainPtr GainPtr(Geometry _) =>
            _backend.Naive(Foci, Amps, (uint)Amps.Length, Constraint);
    }
}
