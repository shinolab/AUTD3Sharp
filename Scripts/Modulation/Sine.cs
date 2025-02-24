using AUTD3Sharp.NativeMethods;
using System;
using static AUTD3Sharp.Units;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Modulation
{
    public class SineOption
    {
        public byte Intensity { get; init; } = 0xFF;
        public byte Offset { get; init; } = 0x80;
        public Angle Phase { get; init; } = 0.0f * rad;
        public bool Clamp { get; init; } = false;
        public SamplingConfig SamplingConfig { get; init; } = SamplingConfig.Freq4K;

        internal NativeMethods.SineOption ToNative() => new()
        {
            intensity = Intensity,
            offset = Offset,
            phase = Phase.ToNative(),
            clamp = Clamp,
            sampling_config_div= SamplingConfig.Division
        };
    }

    internal interface ISamplingMode { }

    internal struct Exact<T> : ISamplingMode
        where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        internal Freq<T> Freq;
    }

    internal struct Nearest : ISamplingMode
    {
        internal Freq<float> Freq;
    }

    public sealed class Sine : Driver.Datagram.Modulation
    {
        internal ISamplingMode Freq;
        public SineOption Option;

        private Sine(ISamplingMode freq, SineOption option)
        {
            Freq = freq;
            Option = option;
        }

        public Sine(Freq<uint> freq, SineOption option) : this(new Exact<uint> { Freq = freq }, option) { }

        public Sine(Freq<float> freq, SineOption option) : this(new Exact<float> { Freq = freq }, option) { }

        public Sine IntoNearest() => Freq switch
        {
            Exact<float> f => new Sine(new Nearest { Freq = f.Freq }, Option),
            Nearest => this,
            _ => throw new AUTDException("Freq type must be float.")
        };

        internal override ModulationPtr ModulationPtr() => Freq switch
        {
            Exact<uint> f => NativeMethodsBase.AUTDModulationSineExact(f.Freq.Hz, Option.ToNative()),
            Exact<float> f => NativeMethodsBase.AUTDModulationSineExactFloat(f.Freq.Hz, Option.ToNative()),
            Nearest f => NativeMethodsBase.AUTDModulationSineNearest(f.Freq.Hz, Option.ToNative()),
            _ => throw AUTDException.InvalidFreqType()
        };
    }
}
