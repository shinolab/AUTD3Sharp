using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    [ComVisible(false)]
    public abstract class Backend
    {
        internal BackendPtr Ptr;

        internal abstract GainPtr Sdp(double[] foci, Amplitude[] amps, ulong size, double alpha, uint repeat, double lambda, EmissionConstraintPtr constraint);

        internal abstract GainPtr Gs(double[] foci, Amplitude[] amps, ulong size, uint repeat, EmissionConstraintPtr constraint);

        internal abstract GainPtr Gspat(double[] foci, Amplitude[] amps, ulong size, uint repeat, EmissionConstraintPtr constraint);

        internal abstract GainPtr Naive(double[] foci, Amplitude[] amps, ulong size, EmissionConstraintPtr constraint);

        internal abstract GainPtr Lm(double[] foci, Amplitude[] amps, ulong size, double eps1, double eps2,
            double tau, uint kMax, double[] initial, EmissionConstraintPtr constraint);
    }
}
