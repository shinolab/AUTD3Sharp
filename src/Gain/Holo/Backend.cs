using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain.Holo
{
    [ComVisible(false)]
    public abstract class Backend
    {
        internal BackendPtr Ptr;

        internal abstract GainPtr Sdp(Vector3[] foci, Amplitude[] amps, uint size, float alpha, uint repeat, float lambda, EmissionConstraintWrap constraint);

        internal abstract GainPtr Gs(Vector3[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint);

        internal abstract GainPtr Gspat(Vector3[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint);

        internal abstract GainPtr Naive(Vector3[] foci, Amplitude[] amps, uint size, EmissionConstraintWrap constraint);

        internal abstract GainPtr Lm(Vector3[] foci, Amplitude[] amps, uint size, float eps1, float eps2,
            float tau, uint kMax, float[] initial, EmissionConstraintWrap constraint);
    }
}
