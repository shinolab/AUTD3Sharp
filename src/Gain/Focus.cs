#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

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
    public sealed class Focus : Driver.Datagram.Gain
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

        internal override GainPtr GainPtr(Geometry geometry) => NativeMethodsBase.AUTDGainFocus(Pos.x, Pos.y, Pos.z, Intensity.Value);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
