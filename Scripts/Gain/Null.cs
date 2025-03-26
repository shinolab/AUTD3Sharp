using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    public sealed class Null : IGain
    {
        GainPtr IGain.GainPtr(Geometry _) => NativeMethodsBase.AUTDGainNull();
    }
}
