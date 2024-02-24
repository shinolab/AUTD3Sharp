#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

#if UNITY_2018_3_OR_NEWER
using Vector3 = UnityEngine.Vector3;
#else
using Vector3 = AUTD3Sharp.Utils.Vector3d;
#endif

namespace AUTD3Sharp.Gain
{
    /// <summary>
    /// Gain to produce single focal pos
    /// </summary>
    [Gain]
    public sealed partial class Focus : Driver.Datagram.Gain.Gain
    {
        public Focus(Vector3 pos)
        {
            Pos = pos;
            Intensity = EmitIntensity.Max;
        }

        /// <summary>
        /// Set amplitude
        /// </summary>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>
        public Focus WithIntensity(byte intensity)
        {
            Intensity = new EmitIntensity(intensity);
            return this;
        }

        /// <summary>
        /// Set amplitude
        /// </summary>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>
        public Focus WithIntensity(EmitIntensity intensity)
        {
            Intensity = intensity;
            return this;
        }

        public Vector3 Pos { get; }

        public EmitIntensity Intensity { get; private set; }

        public Phase PhaseOffset { get; private set; }

        internal override GainPtr GainPtr(Geometry geometry) => NativeMethodsBase.AUTDGainFocus(Pos.x, Pos.y, Pos.z, Intensity.Value, PhaseOffset.Value);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
