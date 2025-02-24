using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public class SamplingConfig : IEquatable<SamplingConfig>
    {
        public static readonly SamplingConfig Freq40K = new(1);
        public static readonly SamplingConfig Freq4K = new(10);

        internal readonly SamplingConfigWrap Inner;

        [ExcludeFromCodeCoverage]
        private SamplingConfig() { }

        internal SamplingConfig(SamplingConfigWrap config) { Inner = config; }

        public SamplingConfig(ushort div) : this(NativeMethodsBase.AUTDSamplingConfigFromDivision(div).Validate()) { }
        public SamplingConfig(Freq<float> f) : this(NativeMethodsBase.AUTDSamplingConfigFromFreq(f.Hz)) { }
        public SamplingConfig(Duration period) : this(NativeMethodsBase.AUTDSamplingConfigFromPeriod(period)) { }

        public static implicit operator SamplingConfig(Freq<float> f) => new(f);
        public static implicit operator SamplingConfig(Duration period) => new(period);

        public SamplingConfig IntoNearest() => new(NativeMethodsBase.AUTDSamplingConfigIntoNearest(Inner));

        public ushort Division => NativeMethodsBase.AUTDSamplingConfigDivision(Inner).Validate();
        public Freq<float> Freq() => NativeMethodsBase.AUTDSamplingConfigFreq(Inner).Validate() * Hz;
        public Duration Period() => NativeMethodsBase.AUTDSamplingConfigPeriod(Inner).Validate();

        public static bool operator ==(SamplingConfig left, SamplingConfig right) => left.Equals(right);
        public static bool operator !=(SamplingConfig left, SamplingConfig right) => !left.Equals(right);
        public bool Equals(SamplingConfig? other) => other is not null && NativeMethodsBase.AUTDSamplingConfigEq(Inner, other.Inner);
        public override bool Equals(object? obj) => obj is SamplingConfig other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Division.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
