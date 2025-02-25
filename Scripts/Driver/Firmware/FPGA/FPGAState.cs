using System;
using System.Diagnostics.CodeAnalysis;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Driver.FPGA.Defined
{
    public class FPGAState : IEquatable<FPGAState>
    {
        private readonly byte _info;

        internal FPGAState(byte info) => _info = info;

        public bool IsThermalAssert() => (_info & (1 << 0)) != 0;

        public Segment CurrentModSegment() => (_info & (1 << 1)) switch
        {
            0 => Segment.S0,
            _ => Segment.S1
        };

        public Segment? CurrentGainSegment()
        {
            if (!IsGainMode()) return null;
            return (_info & (1 << 2)) switch
            {
                0 => Segment.S0,
                _ => Segment.S1
            };
        }

        public Segment? CurrentSTMSegment()
        {
            if (!IsSTMMode()) return null;
            return (_info & (1 << 2)) switch
            {
                0 => Segment.S0,
                _ => Segment.S1
            };
        }

        public bool IsGainMode() => (_info & (1 << 3)) != 0;
        public bool IsSTMMode() => !IsGainMode();

        public static bool operator ==(FPGAState left, FPGAState right) => left.Equals(right);
        public static bool operator !=(FPGAState left, FPGAState right) => !left.Equals(right);
        public bool Equals(FPGAState? other) => other is not null && _info.Equals(other._info);
        public override bool Equals(object? obj) => obj is FPGAState other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => _info.GetHashCode();

        public override string ToString() => $"Thermal assert = {IsThermalAssert()}";
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
