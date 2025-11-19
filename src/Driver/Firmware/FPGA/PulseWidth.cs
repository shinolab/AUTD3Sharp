using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public class PulseWidth : IEquatable<PulseWidth>
    {
        private readonly NativeMethods.PulseWidth _pulseWidth;

        internal NativeMethods.PulseWidth ToNative() => _pulseWidth;

        internal PulseWidth(NativeMethods.PulseWidth inner)
        {
            _pulseWidth = inner;
        }

        public PulseWidth(ushort pulseWidth)
        {
            _pulseWidth = NativeMethodsBase.AUTDPulseWidth(pulseWidth);
        }

        public static PulseWidth FromDuty(float duty) => new(NativeMethodsBase.AUTDPulseWidthFromDuty(duty));

        public ushort Value() => NativeMethodsBase.AUTDPulseWidthPulseWidth(_pulseWidth).Validate();

        public static bool operator ==(PulseWidth left, PulseWidth right) => left.Equals(right);
        public static bool operator !=(PulseWidth left, PulseWidth right) => !left.Equals(right);
        public bool Equals(PulseWidth? other) => other is not null && Value().Equals(other.Value());
        public override bool Equals(object? obj) => obj is PulseWidth other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => _pulseWidth.inner.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
