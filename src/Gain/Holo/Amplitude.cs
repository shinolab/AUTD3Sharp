using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain.Holo
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Amplitude
    {
        internal Amplitude(double value)
        {
            Pascal = value;
        }

        public double Pascal { get; }

        public double SPL => NativeMethodsGainHolo.AUTDGainHoloPascalToSPL(Pascal);

        public static Amplitude NewPascal(double pascal) => new Amplitude(pascal);
        public static Amplitude NewSPL(double spl) => new Amplitude(NativeMethodsGainHolo.AUTDGainHoloSPLToPascal(spl));

        public class UnitPascal
        {
            internal UnitPascal() { }
            public static Amplitude operator *(double a, UnitPascal _) => NewPascal(a);
        }

        public class UnitSPL
        {
            internal UnitSPL() { }
            public static Amplitude operator *(double a, UnitSPL _) => NewSPL(a);
        }

        public static class Units
        {
            public static UnitPascal Pascal { get; } = new UnitPascal();
#pragma warning disable IDE1006
            public static UnitSPL dB { get; } = new UnitSPL();
#pragma warning restore IDE1006
        }
    }
}
