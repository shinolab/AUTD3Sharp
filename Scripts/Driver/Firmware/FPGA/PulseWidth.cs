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
        private ushort _pulseWidth;

        public ushort Value => _pulseWidth;

        private PulseWidth()
        {
            _pulseWidth = 0;
        }

        public PulseWidth(ushort pulseWidth)
        {
            _pulseWidth = NativeMethodsBase.AUTDPulseWidth(pulseWidth).Validate();
        }

        public static PulseWidth FromDuty(float duty) => new PulseWidth
        {
            _pulseWidth = NativeMethodsBase.AUTDPulseWidthFromDuty(duty).Validate()
        };

        public static bool operator ==(PulseWidth left, PulseWidth right) => left.Equals(right);
        public static bool operator !=(PulseWidth left, PulseWidth right) => !left.Equals(right);
        public bool Equals(PulseWidth? other) => other is not null && Value.Equals(other.Value);
        public override bool Equals(object? obj) => obj is PulseWidth other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Value.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
