using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain.Holo
{
    public abstract class Holo<TH>
        where TH : Holo<TH>
    {
        protected readonly Point3[] Foci;
        protected readonly Amplitude[] Amps;

        public EmissionConstraintWrap Constraint { get; private set; }

        protected Holo(EmissionConstraintWrap constraint, IEnumerable<(Point3, Amplitude)> iter)
        {
            Constraint = constraint;
            var tupleIter = iter as (Point3, Amplitude)[] ?? iter.ToArray();
            Foci = tupleIter.Select(x => x.Item1).ToArray();
            Amps = tupleIter.Select(x => x.Item2).ToArray();
        }

        public TH WithConstraint(EmissionConstraintWrap constraint)
        {
            Constraint = constraint;
            return (TH)this;
        }
    }
}
