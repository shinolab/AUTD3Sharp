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

        public Phase(Angle value) : this(NativeMethodsBase.AUTDPhaseFromRad(value.Radian))
        {
        }

        public double Radian => NativeMethodsBase.AUTDPhaseToRad(Value);
    }
}
