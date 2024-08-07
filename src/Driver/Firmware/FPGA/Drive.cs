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

        public static Drive Null => new(new Phase(0), new EmitIntensity(0));
    }
}
