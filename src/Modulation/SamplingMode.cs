using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp.Modulation
{
    internal interface ISamplingMode
    {
        internal ModulationPtr SinePtr(SamplingConfig config, byte intensity, byte offset,
                                         Angle phase, NativeMethods.LoopBehavior loopBehavior);
        internal unsafe ModulationPtr FourierPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior);
        internal unsafe ModulationPtr MixerPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior);
        internal ModulationPtr SquarePtr(SamplingConfig config, byte low, byte high,
                                           float duty, NativeMethods.LoopBehavior loopBehavior);
        internal Freq<float> SineFreq(ModulationPtr ptr);
        internal Freq<float> SquareFreq(ModulationPtr ptr);
    }

    internal sealed class SamplingModeExact : ISamplingMode
    {
        internal SamplingModeExact(Freq<uint> freq)
        {
            Freq = freq;
        }

        internal Freq<uint> Freq { get; }

        ModulationPtr ISamplingMode.SinePtr(SamplingConfig config, byte intensity, byte offset,
            Angle phase, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineExact(Freq.Hz, config.Inner, intensity, offset, phase.Radian, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.FourierPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierExact(p, len, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.MixerPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationMixerExact(p, len, loopBehavior).Validate();

        ModulationPtr ISamplingMode.SquarePtr(SamplingConfig config, byte low, byte high,
            float duty, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSquareExact(Freq.Hz, config.Inner, low, high, duty, loopBehavior).Validate();

        Freq<float> ISamplingMode.SineFreq(ModulationPtr ptr) => (float)NativeMethodsBase.AUTDModulationSineExactFreq(ptr) * Hz;
        Freq<float> ISamplingMode.SquareFreq(ModulationPtr ptr) => (float)NativeMethodsBase.AUTDModulationSquareExactFreq(ptr) * Hz;
    }

    internal sealed class SamplingModeExactFloat : ISamplingMode
    {
        internal SamplingModeExactFloat(Freq<float> freq)
        {
            Freq = freq;
        }

        internal Freq<float> Freq { get; }

        ModulationPtr ISamplingMode.SinePtr(SamplingConfig config, byte intensity, byte offset,
            Angle phase, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineExactFloat(Freq.Hz, config.Inner, intensity, offset, phase.Radian, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.FourierPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierExactFloat(p, len, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.MixerPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationMixerExactFloat(p, len, loopBehavior).Validate();

        ModulationPtr ISamplingMode.SquarePtr(SamplingConfig config, byte low, byte high,
            float duty, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSquareExactFloat(Freq.Hz, config.Inner, low, high, duty, loopBehavior).Validate();

        Freq<float> ISamplingMode.SineFreq(ModulationPtr ptr) => NativeMethodsBase.AUTDModulationSineExactFloatFreq(ptr) * Hz;
        Freq<float> ISamplingMode.SquareFreq(ModulationPtr ptr) => NativeMethodsBase.AUTDModulationSquareExactFloatFreq(ptr) * Hz;
    }

    internal sealed class SamplingModeNearest : ISamplingMode
    {
        internal SamplingModeNearest(Freq<float> freq)
        {
            Freq = freq;
        }

        internal Freq<float> Freq { get; }

        ModulationPtr ISamplingMode.SinePtr(SamplingConfig config, byte intensity, byte offset,
            Angle phase, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineNearest(Freq.Hz, config.Inner, intensity, offset, phase.Radian, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.FourierPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierNearest(p, len, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.MixerPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationMixerNearest(p, len, loopBehavior).Validate();

        ModulationPtr ISamplingMode.SquarePtr(SamplingConfig config, byte low, byte high,
            float duty, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSquareNearest(Freq.Hz, config.Inner, low, high, duty, loopBehavior).Validate();

        Freq<float> ISamplingMode.SineFreq(ModulationPtr ptr) => NativeMethodsBase.AUTDModulationSineNearestFreq(ptr) * Hz;
        Freq<float> ISamplingMode.SquareFreq(ModulationPtr ptr) => NativeMethodsBase.AUTDModulationSquareNearestFreq(ptr) * Hz;
    }

}