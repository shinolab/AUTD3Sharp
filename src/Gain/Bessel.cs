#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using AUTD3Sharp.Driver.Geometry;
using AUTD3Sharp.NativeMethods;

#if UNITY_2018_3_OR_NEWER
using Vector3 = UnityEngine.Vector3;
#else
using Vector3 = AUTD3Sharp.Utils.Vector3d;
#endif

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp.Gain
{
    /// <summary>
    /// Gain to produce a Bessel beam
    /// </summary>
    public sealed class Bessel : Driver.Datagram.Gain
    {
        public Bessel(Vector3 pos, Vector3 dir, float_t theta)
        {
            Pos = pos;
            Dir = dir;
            Theta = theta;
            Intensity = EmitIntensity.Max;
        }

        /// <summary>
        /// Set amplitude
        /// </summary>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>
        public Bessel WithIntensity(byte intensity)
        {
            Intensity = new EmitIntensity(intensity);
            return this;
        }

        /// <summary>
        /// Set amplitude
        /// </summary>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>
        public Bessel WithIntensity(EmitIntensity intensity)
        {
            Intensity = intensity;
            return this;
        }

        public Vector3 Pos { get; }

        public Vector3 Dir { get; }

        public float_t Theta { get; }

        public EmitIntensity Intensity { get; private set; }

        internal override GainPtr GainPtr(Geometry geometry) => NativeMethodsBase.AUTDGainBessel(Pos.x, Pos.y, Pos.z, Dir.x, Dir.y, Dir.z, Theta, Intensity.Value);
    }
}
