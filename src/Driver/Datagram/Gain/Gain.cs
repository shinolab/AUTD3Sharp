using System.Runtime.InteropServices;

using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Gain
{
    [ComVisible(false)]
    public interface IGain
    {
        public GainPtr GainPtr(Geometry geometry);
    }
}
