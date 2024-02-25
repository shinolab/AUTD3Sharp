#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif


using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

#if UNITY_2018_3_OR_NEWER
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
#else
using Vector3 = AUTD3Sharp.Utils.Vector3d;
#endif

namespace AUTD3Sharp.Gain
{
    /// <summary>
    /// Gain to produce a plane wave
    /// </summary>
    [Gain]
    public sealed partial class Plane
    {
        public Plane(Vector3 dir)
        {
            Dir = dir;
            Intensity = EmitIntensity.Max;
            Phase = new Phase(0);
        }

        /// <summary>
        /// Set amplitude
        /// </summary>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>

        public Plane WithIntensity(byte intensity)
        {
            Intensity = new EmitIntensity(intensity);
            return this;
        }
        /// <summary>
        /// Set amplitude
        /// </summary>
        /// <param name="intensity">Emission intensity</param>
        /// <returns></returns>
        public Plane WithIntensity(EmitIntensity intensity)
        {
            Intensity = intensity;
            return this;
        }

        public Plane WithPhase(Phase phase)
        {
            Phase = phase;
            return this;
        }

        public Vector3 Dir { get; }
        public Phase Phase { get; private set; }
        public EmitIntensity Intensity { get; private set; }

        private GainPtr GainPtr(Geometry geometry) => NativeMethodsBase.AUTDGainPlane(Dir.x, Dir.y, Dir.z, Intensity.Value, Phase.Value);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
