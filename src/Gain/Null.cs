using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain
{
    /// <summary>
    /// Gain to output nothing
    /// </summary>
    [Gain]
    public sealed partial class Null : Driver.Datagram.Gain.Gain
    {
        internal override GainPtr GainPtr(Geometry geometry) => NativeMethodsBase.AUTDGainNull();
    }
}
