#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

using System;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public readonly struct SamplingConfiguration
    {
        internal SamplingConfigurationRaw Internal { get; }

        internal SamplingConfiguration(SamplingConfigurationRaw @internal)
        {
            Internal = @internal;
        }

        public static SamplingConfiguration FromFrequencyDivision(uint div)
        {
            return new SamplingConfiguration(NativeMethodsDef.AUTDSamplingConfigFromFrequencyDivision(div).Validate());
        }

        public static SamplingConfiguration FromFrequency(float_t f)
        {
            return new SamplingConfiguration(NativeMethodsDef.AUTDSamplingConfigFromFrequency(f).Validate());
        }

        public static SamplingConfiguration FromPeriod(TimeSpan p)
        {
            return new SamplingConfiguration(NativeMethodsDef.AUTDSamplingConfigFromPeriod((ulong)(p.TotalSeconds * 1000 *
                1000 * 1000)).Validate());
        }

        public uint FrequencyDivision => NativeMethodsDef.AUTDSamplingConfigFrequencyDivision(Internal);

        public float_t Frequency => NativeMethodsDef.AUTDSamplingConfigFrequency(Internal);

        public TimeSpan Period => TimeSpan.FromSeconds(NativeMethodsDef.AUTDSamplingConfigPeriod(Internal) / 1000.0 / 1000.0 / 1000.0);
    }
}
