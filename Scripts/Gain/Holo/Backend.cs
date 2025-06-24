using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain.Holo
{
    [ComVisible(false)]
    public abstract class Backend
    {
        internal BackendPtr Ptr;

        internal abstract GainPtr Gs((Point3, Amplitude)[] foci, NativeMethods.GSOption option);
        internal abstract GainPtr Gspat((Point3, Amplitude)[] foci, NativeMethods.GSPATOption option);
        internal abstract GainPtr Naive((Point3, Amplitude)[] foci, NativeMethods.NaiveOption option);
        internal abstract GainPtr Lm((Point3, Amplitude)[] foci, NativeMethods.LMOption option);
    }
}
