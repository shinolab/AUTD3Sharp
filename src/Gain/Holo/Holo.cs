using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain.Holo
{
    public abstract class Holo<TH>
        where TH : Holo<TH>
    {
        protected readonly List<double> Foci = new List<double>();
        protected readonly List<Amplitude> Amps = new List<Amplitude>();
        public EmissionConstraint Constraint { get; private set; }

        protected Holo(EmissionConstraint constraint)
        {
            Constraint = constraint;
        }

        public TH AddFocus(Vector3d focus, Amplitude amp)
        {
            Foci.Add(focus.X);
            Foci.Add(focus.Y);
            Foci.Add(focus.Z);
            Amps.Add(amp);
            return (TH)this;
        }

        /// <summary>
        /// Add foci
        /// </summary>
        /// <param name="iter">Enumerable of foci and amps</param>
        public TH AddFociFromIter(IEnumerable<(Vector3d, Amplitude)> iter)
        {
            return (TH)iter.Aggregate(this, (holo, point) => holo.AddFocus(point.Item1, point.Item2));
        }

        /// <summary>
        /// Set amplitude constraint
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        public TH WithConstraint(EmissionConstraint constraint)
        {
            Constraint = constraint;
            return (TH)this;
        }
    }
}
