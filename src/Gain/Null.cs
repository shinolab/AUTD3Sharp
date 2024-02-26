
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain
{
    /// <summary>
    /// Gain to output nothing
    /// </summary>
    [Gain]
    public sealed partial class Null
    {
        private static GainPtr GainPtr(Geometry _) => NativeMethodsBase.AUTDGainNull();
    }
}
