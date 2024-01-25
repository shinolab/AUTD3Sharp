#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System.Collections.Generic;
using System.Linq;

#if UNITY_2018_3_OR_NEWER
using Vector3 = UnityEngine.Vector3;
#else
using Vector3 = AUTD3Sharp.Utils.Vector3d;
#endif

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp.Gain.Holo
{
    public abstract class Holo<TH> : Driver.Datagram.Gain
        where TH : Holo<TH>
    {
        protected readonly List<float_t> Foci = new List<float_t>();
        protected readonly List<Amplitude> Amps = new List<Amplitude>();
        public EmissionConstraint Constraint { get; private set; }

        protected Holo(EmissionConstraint constraint)
        {
            Constraint = constraint;
        }

        public TH AddFocus(Vector3 focus, Amplitude amp)
        {
            Foci.Add(focus.x);
            Foci.Add(focus.y);
            Foci.Add(focus.z);
            Amps.Add(amp);
            return (TH)this;
        }

        /// <summary>
        /// Add foci
        /// </summary>
        /// <param name="iter">Enumerable of foci and amps</param>
        public TH AddFociFromIter(IEnumerable<(Vector3, Amplitude)> iter)
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
