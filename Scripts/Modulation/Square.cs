using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Modulation
{
    public class SquareOption
    {
        public byte Low { get; init; } = 0x00;
        public byte High { get; init; } = 0xFF;
        public float Duty { get; init; } = 0.5f;
        public SamplingConfig SamplingConfig { get; init; } = SamplingConfig.Freq4K;

        internal NativeMethods.SquareOption ToNative() => new()
        {
            low = Low,
            high = High,
            duty = Duty,
            sampling_config_div = SamplingConfig.Division
        };
    }

    public sealed class Square : Driver.Datagram.Modulation
    {
        internal ISamplingMode Freq;
        public SquareOption Option;

        private Square(ISamplingMode freq, SquareOption option)
        {
            Freq = freq;
            Option = option;
        }

        public Square(Freq<uint> freq, SquareOption option) : this(new Exact<uint> { Freq = freq }, option) { }

        public Square(Freq<float> freq, SquareOption option) : this(new Exact<float> { Freq = freq }, option) { }

        public Square IntoNearest() => Freq switch
        {
            Exact<float> f => new Square(new Nearest { Freq = f.Freq }, Option),
            Nearest => this,
            _ => throw new AUTDException("Freq type must be float.")
        };

        internal override ModulationPtr ModulationPtr() => Freq switch
        {
            Exact<uint> f => NativeMethodsBase.AUTDModulationSquareExact(f.Freq.Hz, Option.ToNative()),
            Exact<float> f => NativeMethodsBase.AUTDModulationSquareExactFloat(f.Freq.Hz, Option.ToNative()),
            Nearest f => NativeMethodsBase.AUTDModulationSquareNearest(f.Freq.Hz, Option.ToNative()),
            _ => throw AUTDException.InvalidFreqType()
        };
    }
}
