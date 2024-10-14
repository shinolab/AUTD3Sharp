using System.Runtime.InteropServices;

namespace AUTD3Sharp
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Drive
    {
        public Phase Phase { get; set; }
        public EmitIntensity Intensity { get; set; }

        public Drive(Phase phase, EmitIntensity intensity)
        {
            Phase = phase;
            Intensity = intensity;
        }

        public static implicit operator Drive((Phase, EmitIntensity) d) => new(d.Item1, d.Item2);
        public static implicit operator Drive((EmitIntensity, Phase) d) => new(d.Item2, d.Item1);
        public static implicit operator Drive(EmitIntensity d) => new(new Phase(0x00), d);
        public static implicit operator Drive(Phase d) => new(d, EmitIntensity.Max);

        public static Drive Null => new(new Phase(0), new EmitIntensity(0));
    }
}
