using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain
{
    [Gain]
    [Builder]
    public sealed partial class Bessel
    {
        public Bessel(Vector3 pos, Vector3 dir, Angle theta)
        {
            Pos = pos;
            Dir = dir;
            Theta = theta;
            Intensity = EmitIntensity.Max;
            PhaseOffset = new Phase(0);
        }

        public Vector3 Pos { get; }

        public Vector3 Dir { get; }

        public Angle Theta { get; }

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [Property]
        public Phase PhaseOffset { get; private set; }

        private GainPtr GainPtr(Geometry _) => NativeMethodsBase.AUTDGainBessel(Pos, Dir, Theta.Radian, Intensity.Value, PhaseOffset.Value);
    }
}
