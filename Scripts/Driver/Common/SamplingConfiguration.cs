using System;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public readonly struct SamplingConfiguration
    {
        public SamplingConfigurationRaw Internal { get; }

        public SamplingConfiguration(SamplingConfigurationRaw @internal)
        {
            Internal = @internal;
        }

        public static SamplingConfiguration FromFrequencyDivision(uint div)
        {
            return new SamplingConfiguration(NativeMethodsDef.AUTDSamplingConfigFromFrequencyDivision(div).Validate());
        }

        public static SamplingConfiguration FromFrequency(double f)
        {
            return new SamplingConfiguration(NativeMethodsDef.AUTDSamplingConfigFromFrequency(f).Validate());
        }

        public static SamplingConfiguration FromPeriod(TimeSpan p)
        {
            return new SamplingConfiguration(NativeMethodsDef.AUTDSamplingConfigFromPeriod((ulong)(p.TotalSeconds * 1000 *
                1000 * 1000)).Validate());
        }

        public uint FrequencyDivision => NativeMethodsDef.AUTDSamplingConfigFrequencyDivision(Internal);

        public double Frequency => NativeMethodsDef.AUTDSamplingConfigFrequency(Internal);

        public TimeSpan Period => TimeSpan.FromSeconds(NativeMethodsDef.AUTDSamplingConfigPeriod(Internal) / 1000.0 / 1000.0 / 1000.0);
    }
}
