using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain
{
    [Gain]
    [Builder]
    public sealed partial class Plane
    {
        public Plane(Vector3d dir)
        {
            Dir = dir;
            Intensity = EmitIntensity.Max;
            PhaseOffset = new Phase(0);
        }

        public Vector3d Dir { get; }

        [Property]
        public Phase PhaseOffset { get; private set; }

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        private GainPtr GainPtr(Geometry _) => NativeMethodsBase.AUTDGainPlane(Dir.X, Dir.Y, Dir.Z, Intensity.Value, PhaseOffset.Value);
    }
}
