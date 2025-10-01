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
        private NativeMethods.PulseWidth _pulseWidth;

        internal NativeMethods.PulseWidth ToNative() => _pulseWidth;

        private PulseWidth()
        {
            _pulseWidth = new NativeMethods.PulseWidth { inner = 0 };
        }

        internal static PulseWidth FromNative(ulong pw) => new PulseWidth
        {
            _pulseWidth = new NativeMethods.PulseWidth { inner = pw }
        };

        public PulseWidth(uint pulseWidth)
        {
            _pulseWidth = NativeMethodsBase.AUTDPulseWidth(pulseWidth);
        }

        public static PulseWidth FromDuty(float duty) => new PulseWidth
        {
            _pulseWidth = NativeMethodsBase.AUTDPulseWidthFromDuty(duty).Validate()
        };

        public ushort Value(uint period) => NativeMethodsBase.AUTDPulseWidthPulseWidth(_pulseWidth, period).Validate();

        public static bool operator ==(PulseWidth left, PulseWidth right) => left.Equals(right);
        public static bool operator !=(PulseWidth left, PulseWidth right) => !left.Equals(right);
        public bool Equals(PulseWidth? other) => other is not null && Value(512).Equals(other.Value(512));
        public override bool Equals(object? obj) => obj is PulseWidth other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => _pulseWidth.inner.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
