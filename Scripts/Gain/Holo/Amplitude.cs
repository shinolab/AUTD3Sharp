using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Gain.Holo
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Amplitude : IEquatable<Amplitude>
    {
        private readonly float _value;

        internal Amplitude(float value)
        {
            _value = value;
        }

        public float Pascal() => _value;
        public float SPL() => NativeMethodsGainHolo.AUTDGainHoloPascalToSPL(_value);

        private static Amplitude NewPascal(float pascal) => new(pascal);
        private static Amplitude NewSPL(float spl) => new(NativeMethodsGainHolo.AUTDGainHoloSPLToPascal(spl));

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

        public static bool operator ==(Amplitude left, Amplitude right) => left.Equals(right);
        public static bool operator !=(Amplitude left, Amplitude right) => !left.Equals(right);
        public bool Equals(Amplitude other) => _value.Equals(other._value);
        public override bool Equals(object? obj) => obj is Amplitude other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => _value.GetHashCode();
    }
}

namespace AUTD3Sharp
{
    public static partial class Units
    {
        public static Gain.Holo.Amplitude.UnitPascal Pa { get; } = new();
#pragma warning disable IDE1006
        public static Gain.Holo.Amplitude.UnitSPL dB { get; } = new();
#pragma warning restore IDE1006
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
