#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

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
    [Gain]
    [Builder]
    public sealed partial class Bessel
    {
        public Bessel(Vector3 pos, Vector3 dir, float_t theta)
        {
            Pos = pos;
            Dir = dir;
            Theta = theta;
            Intensity = EmitIntensity.Max;
            PhaseOffset = new Phase(0);
        }

        public Vector3 Pos { get; }

        public Vector3 Dir { get; }

        public float_t Theta { get; }

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [Property]
        public Phase PhaseOffset { get; private set; }

        private GainPtr GainPtr(Geometry _) => NativeMethodsBase.AUTDGainBessel(Pos.x, Pos.y, Pos.z, Dir.x, Dir.y, Dir.z, Theta, Intensity.Value, PhaseOffset.Value);
    }
}
