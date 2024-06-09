using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp.Gain
{
    [Gain]
    [Builder]
    public sealed partial class Focus
    {
        public Focus(Vector3 pos)
        {
            Pos = pos;
            Intensity = EmitIntensity.Max;
            PhaseOffset = new Phase(0);
        }

        public Vector3 Pos { get; }

        [Property(EmitIntensity = true)]
        public EmitIntensity Intensity { get; private set; }

        [Property]
        public Phase PhaseOffset { get; private set; }

        private GainPtr GainPtr(Geometry _) => NativeMethodsBase.AUTDGainFocus(Pos.X, Pos.Y, Pos.Z, Intensity.Value, PhaseOffset.Value);
    }
}
