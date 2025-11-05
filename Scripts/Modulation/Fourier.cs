using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Modulation
{
    public class FourierOption
    {
        public float? ScaleFactor { get; init; } = null;
        public bool Clamp { get; init; } = false;
        public byte Offset { get; init; } = 0;

        internal NativeMethods.FourierOption ToNative() => new()
        {
            has_scale_factor = ScaleFactor.HasValue,
            scale_factor = ScaleFactor ?? float.NaN,
            clamp = Clamp,
            offset = Offset
        };
    }

    public sealed class Fourier : Driver.Datagram.Modulation
    {
        public Sine[] Components;
        public FourierOption Option;

        public Fourier(Sine[] components, FourierOption option)
        {
            Components = components;
            Option = option;
        }

        internal override ModulationPtr ModulationPtr()
        {
            [ExcludeFromCodeCoverage]
            static int FreqType(ISamplingMode mode) => mode switch
            {
                Exact<uint> => 0,
                Exact<float> => 1,
                Nearest => 2,
                _ => 3
            };

            if (Components.Select(s => FreqType(s.Freq)).Distinct().Count() != 1)
                throw new AUTDException("All components must have the same frequency type.");

            var sineOption = Components.Select(s => s.Option.ToNative()).ToArray();
            unsafe
            {
                fixed (NativeMethods.SineOption* pOption = &sineOption[0])
                {
                    switch (Components[0].Freq)
                    {
                        case Exact<uint>:
                            var sineFreq = Components.Select(s => ((Exact<uint>)s.Freq).Freq.Hz).ToArray();
                            fixed (uint* pFreq = &sineFreq[0])
                                return NativeMethodsBase.AUTDModulationFourierExact(pFreq, pOption,
                                    (uint)Components.Length, Option.ToNative());
                        case Exact<float>:
                            var sineFreqF = Components.Select(s => ((Exact<float>)s.Freq).Freq.Hz).ToArray();
                            fixed (float* pFreq = &sineFreqF[0])
                                return NativeMethodsBase.AUTDModulationFourierExactFloat(pFreq, pOption,
                                    (uint)Components.Length, Option.ToNative());
                        case Nearest:
                            var sineFreqN = Components.Select(s => ((Nearest)s.Freq).Freq.Hz).ToArray();
                            fixed (float* pFreq = &sineFreqN[0])
                                return NativeMethodsBase.AUTDModulationFourierNearest(pFreq, pOption,
                                    (uint)Components.Length, Option.ToNative());
                        default:
                            throw AUTDException.InvalidFreqType();
                    }
                }
            }
        }
    }
}
