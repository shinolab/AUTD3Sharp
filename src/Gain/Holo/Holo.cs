using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain.Holo
{
    public abstract class Holo<TH>
        where TH : Holo<TH>
    {
        protected readonly List<float> Foci = new List<float>();
        protected readonly List<Amplitude> Amps = new List<Amplitude>();
        public EmissionConstraintWrap Constraint { get; private set; }

        protected Holo(EmissionConstraintWrap constraint)
        {
            Constraint = constraint;
        }

        public TH AddFocus(Vector3 focus, Amplitude amp)
        {
            Foci.Add(focus.X);
            Foci.Add(focus.Y);
            Foci.Add(focus.Z);
            Amps.Add(amp);
            return (TH)this;
        }

        public TH AddFociFromIter(IEnumerable<(Vector3, Amplitude)> iter)
        {
            return (TH)iter.Aggregate(this, (holo, point) => holo.AddFocus(point.Item1, point.Item2));
        }

        public TH WithConstraint(EmissionConstraintWrap constraint)
        {
            Constraint = constraint;
            return (TH)this;
        }
    }
}
