using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Static
    {
        public Static()
        {
            Intensity = 0xFF;
        }

        private Static(byte intensity)
        {
            Intensity = intensity;
        }

        public static Static WithIntensity(byte intensity) => new(intensity);

        public byte Intensity { get; }

        private ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationStatic(Intensity, LoopBehavior);
    }
}
