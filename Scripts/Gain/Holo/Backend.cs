#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp.Gain.Holo
{
    [ComVisible(false)]
    public abstract class Backend
    {
        internal BackendPtr Ptr;

        internal abstract GainPtr Sdp(float_t[] foci, Amplitude[] amps, ulong size, float_t alpha, uint repeat, float_t lambda, EmissionConstraintPtr constraint);

        internal abstract GainPtr Gs(float_t[] foci, Amplitude[] amps, ulong size, uint repeat, EmissionConstraintPtr constraint);

        internal abstract GainPtr Gspat(float_t[] foci, Amplitude[] amps, ulong size, uint repeat, EmissionConstraintPtr constraint);

        internal abstract GainPtr Naive(float_t[] foci, Amplitude[] amps, ulong size, EmissionConstraintPtr constraint);

        internal abstract GainPtr Lm(float_t[] foci, Amplitude[] amps, ulong size, float_t eps1, float_t eps2,
            float_t tau, uint kMax, float_t[] initial, EmissionConstraintPtr constraint);
    }
}
