using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain
{
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

        [Property(Phase = true)]
        public Phase PhaseOffset { get; private set; }

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        private GainPtr GainPtr(Geometry _) => NativeMethodsBase.AUTDGainPlane(Dir, Intensity.Value, PhaseOffset.Value);
    }
}
