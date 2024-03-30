using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct EmitIntensity
    {
        public byte Value { get; }

        public static readonly EmitIntensity Max = new EmitIntensity(0xFF);
        public static readonly EmitIntensity Min = new EmitIntensity(0x00);

        public static readonly double DefaultCorrectedAlpha = NativeMethodsDef.DEFAULT_CORRECTED_ALPHA;

        public EmitIntensity(byte value)
        {
            Value = value;
        }

        public static EmitIntensity WithCorrectionAlpha(byte value, double alpha) => new EmitIntensity(NativeMethodsDef.AUTDEmitIntensityWithCorrectionAlpha(value, alpha));

        public static EmitIntensity WithCorrection(byte value) => WithCorrectionAlpha(value, NativeMethodsDef.DEFAULT_CORRECTED_ALPHA);

        public static EmitIntensity operator /(EmitIntensity a, int b)
        {
            return new EmitIntensity((byte)(a.Value / b));
        }
    }
}
