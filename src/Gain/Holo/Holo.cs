using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain.Holo
{
    public abstract class Holo<TH>
        where TH : Holo<TH>
    {
        protected readonly float[] Foci;
        protected readonly Amplitude[] Amps;

        public EmissionConstraintWrap Constraint { get; private set; }

        protected Holo(EmissionConstraintWrap constraint, IEnumerable<(Vector3, Amplitude)> iter)
        {
            Constraint = constraint;
            Foci = iter.SelectMany(x => x.Item1).ToArray();
            Amps = iter.Select(x => x.Item2).ToArray();
        }

        public TH WithConstraint(EmissionConstraintWrap constraint)
        {
            Constraint = constraint;
            return (TH)this;
        }
    }
}
