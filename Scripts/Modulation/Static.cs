using System.Diagnostics.CodeAnalysis;
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

        [ExcludeFromCodeCoverage]
        public static Static WithIntensity(byte intensity)
        {
            return new Static(new EmitIntensity(intensity));
        }

        public static Static WithIntensity(EmitIntensity intensity)
        {
            return new Static(intensity);
        }

        public EmitIntensity Intensity { get; }

        private ModulationPtr ModulationPtr(Geometry _) => NativeMethodsBase.AUTDModulationStatic(Intensity.Value, LoopBehavior);
    }
}
