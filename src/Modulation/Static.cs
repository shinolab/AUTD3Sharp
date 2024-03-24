using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Without modulation
    /// </summary>
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

        /// <summary>
        /// Set amplitude
        /// </summary>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public static Static WithIntensity(byte intensity)
        {
            return new Static(new EmitIntensity(intensity));
        }

        /// <summary>
        /// Set amplitude
        /// </summary>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>
        public static Static WithIntensity(EmitIntensity intensity)
        {
            return new Static(intensity);
        }

        public EmitIntensity Intensity { get; }

        private ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationStatic(Intensity.Value, LoopBehavior.Internal);
    }
}
