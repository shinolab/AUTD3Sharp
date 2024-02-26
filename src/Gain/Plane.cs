#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif


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
    /// Gain to produce a plane wave
    /// </summary>
    [Gain]
    [Builder]
    public sealed partial class Plane
    {
        public Plane(Vector3 dir)
        {
            Dir = dir;
            Intensity = EmitIntensity.Max;
            PhaseOffset = new Phase(0);
        }

        public Vector3 Dir { get; }

        [Property]
        public Phase PhaseOffset { get; private set; }

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        private GainPtr GainPtr(Geometry geometry) => NativeMethodsBase.AUTDGainPlane(Dir.x, Dir.y, Dir.z, Intensity.Value, PhaseOffset.Value);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
