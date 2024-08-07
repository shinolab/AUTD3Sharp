using System.Runtime.InteropServices;

namespace AUTD3Sharp
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct EmitIntensity
    {
        public byte Value { get; }

        public static readonly EmitIntensity Max = new(0xFF);
        public static readonly EmitIntensity Min = new(0x00);

        public EmitIntensity(byte value)
        {
            Value = value;
        }

        public static EmitIntensity operator /(EmitIntensity a, int b)
        {
            return new EmitIntensity((byte)(a.Value / b));
        }
    }
}
