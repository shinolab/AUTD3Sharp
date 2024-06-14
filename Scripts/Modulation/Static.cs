using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Static
    {
        public Static()
        {
            Intensity = EmitIntensity.Max;
        }

        private Static(EmitIntensity intensity)
        {
            Intensity = intensity;
        }

        public static Static WithIntensity(EmitIntensity intensity) => new Static(intensity);

        public static Static WithIntensity(byte intensity) => Static.WithIntensity(new EmitIntensity(intensity));

        public EmitIntensity Intensity { get; }

        private ModulationPtr ModulationPtr(Geometry _) => NativeMethodsBase.AUTDModulationStatic(Intensity.Value, LoopBehavior);
    }
}
