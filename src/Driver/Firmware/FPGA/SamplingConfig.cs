using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp
{
    public class SamplingConfig : IEquatable<SamplingConfig>
    {
        public static readonly SamplingConfig Freq40K = new(1);
        public static readonly SamplingConfig Freq4K = new(10);

        internal readonly NativeMethods.SamplingConfig Inner;

        [ExcludeFromCodeCoverage]
        private SamplingConfig() { }

        internal SamplingConfig(NativeMethods.SamplingConfig config) { Inner = config; }

        public SamplingConfig(ushort div) : this(NativeMethodsBase.AUTDSamplingConfigFromDivision(div).Validate()) { }
        public SamplingConfig(Freq<uint> f) : this(NativeMethodsBase.AUTDSamplingConfigFromFreq(f.Hz).Validate()) { }
        public SamplingConfig(Freq<float> f) : this(NativeMethodsBase.AUTDSamplingConfigFromFreqF(f.Hz).Validate()) { }
        public SamplingConfig(Duration period) : this(NativeMethodsBase.AUTDSamplingConfigFromPeriod(period).Validate()) { }

        public static implicit operator SamplingConfig(Freq<uint> f) => new(f);
        public static implicit operator SamplingConfig(Freq<float> f) => new(f);
        public static implicit operator SamplingConfig(Duration period) => new(period);

        public static SamplingConfig Nearest(Freq<float> f) => new(NativeMethodsBase.AUTDSamplingConfigFromFreqNearest(f.Hz));
        public static SamplingConfig Nearest(Duration period) => new(NativeMethodsBase.AUTDSamplingConfigFromPeriodNearest(period));

        public ushort Division => Inner.division;
        public Freq<float> Freq() => NativeMethodsBase.AUTDSamplingConfigFreq(Inner) * Hz;
        public Duration Period() => NativeMethodsBase.AUTDSamplingConfigPeriod(Inner);

        public static bool operator ==(SamplingConfig left, SamplingConfig right) => left.Equals(right);
        public static bool operator !=(SamplingConfig left, SamplingConfig right) => !left.Equals(right);
        public bool Equals(SamplingConfig? other) => other is not null && Inner.Equals(other.Inner);
        public override bool Equals(object? obj) => obj is SamplingConfig other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Inner.GetHashCode();
    }
}
