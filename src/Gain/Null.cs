using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    /// <summary>
    /// Gain to output nothing
    /// </summary>
    public sealed class Null : Driver.Datagram.Gain
    {
        internal override GainPtr GainPtr(Geometry geometry) => NativeMethodsBase.AUTDGainNull();
    }
}
