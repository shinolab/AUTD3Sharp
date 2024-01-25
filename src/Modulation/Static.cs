#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Without modulation
    /// </summary>
    public sealed class Static : Driver.Datagram.Modulation
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

        internal override ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationStatic(Intensity.Value);
    }
}
