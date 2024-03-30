using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Phase
    {
        public byte Value { get; }

        public Phase(byte value)
        {
            Value = value;
        }

        public double Radian => NativeMethodsDef.AUTDPhaseToRad(Value);

        public static Phase FromRad(double value) => new Phase(NativeMethodsDef.AUTDPhaseFromRad(value));
    }
}
