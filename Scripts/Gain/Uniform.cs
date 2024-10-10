using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain
{
    [Gain]
    public sealed partial class Uniform
    {
        public Uniform(Drive d)
        {
            Drive = d;
        }

        public Drive Drive { get; }

        private GainPtr GainPtr(Geometry _) => NativeMethodsBase.AUTDGainUniform(Drive.Intensity.Value, Drive.Phase.Value);
    }
}
