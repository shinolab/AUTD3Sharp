using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    [ComVisible(false)]
    public abstract class Backend
    {
        internal BackendPtr Ptr;

        internal abstract GainPtr Sdp(float[] foci, Amplitude[] amps, uint size, float alpha, uint repeat, float lambda, EmissionConstraintWrap constraint);

        internal abstract GainPtr Gs(float[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint);

        internal abstract GainPtr Gspat(float[] foci, Amplitude[] amps, uint size, uint repeat, EmissionConstraintWrap constraint);

        internal abstract GainPtr Naive(float[] foci, Amplitude[] amps, uint size, EmissionConstraintWrap constraint);

        internal abstract GainPtr Lm(float[] foci, Amplitude[] amps, uint size, float eps1, float eps2,
            float tau, uint kMax, float[] initial, EmissionConstraintWrap constraint);
    }
}
