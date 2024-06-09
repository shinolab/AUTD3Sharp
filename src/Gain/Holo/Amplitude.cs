using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Amplitude
    {
        internal Amplitude(float value)
        {
            Pascal = value;
        }

        public float Pascal { get; }

        public float SPL => NativeMethodsGainHolo.AUTDGainHoloPascalToSPL(Pascal);

        public static Amplitude NewPascal(float pascal) => new Amplitude(pascal);
        public static Amplitude NewSPL(float spl) => new Amplitude(NativeMethodsGainHolo.AUTDGainHoloSPLToPascal(spl));

        public class UnitPascal
        {
            internal UnitPascal() { }
            public static Amplitude operator *(float a, UnitPascal _) => NewPascal(a);
        }

        public class UnitSPL
        {
            internal UnitSPL() { }
            public static Amplitude operator *(float a, UnitSPL _) => NewSPL(a);
        }
    }
}

namespace AUTD3Sharp
{
    public static partial class Units
    {
        public static Gain.Holo.Amplitude.UnitPascal Pa { get; } = new Gain.Holo.Amplitude.UnitPascal();
#pragma warning disable IDE1006
        public static Gain.Holo.Amplitude.UnitSPL dB { get; } = new Gain.Holo.Amplitude.UnitSPL();
#pragma warning restore IDE1006
    }
}
