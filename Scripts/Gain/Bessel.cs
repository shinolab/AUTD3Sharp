using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain
{
    [Gain]
    [Builder]
    public sealed partial class Bessel
    {
        public Bessel(Vector3d pos, Vector3d dir, double theta)
        {
            Pos = pos;
            Dir = dir;
            Theta = theta;
            Intensity = EmitIntensity.Max;
            PhaseOffset = new Phase(0);
        }

        public Vector3d Pos { get; }

        public Vector3d Dir { get; }

        public double Theta { get; }

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [Property]
        public Phase PhaseOffset { get; private set; }

        private GainPtr GainPtr(Geometry _) => NativeMethodsBase.AUTDGainBessel(Pos.X, Pos.Y, Pos.Z, Dir.X, Dir.Y, Dir.Z, Theta, Intensity.Value, PhaseOffset.Value);
    }
}
