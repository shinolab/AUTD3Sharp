using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp.Modulation
{
    internal interface ISamplingMode
    {
        internal ModulationPtr SinePtr(SamplingConfig config, byte intensity, byte offset,
                                         Angle phase, bool clamp, NativeMethods.LoopBehavior loopBehavior);
        internal unsafe ModulationPtr FourierPtr(ModulationPtr* p, uint len, bool clamp, float? scaleFactor, byte offset, NativeMethods.LoopBehavior loopBehavior);
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
            Angle phase, bool clamp, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineExact(Freq.Hz, config.Inner, intensity, offset, phase.Radian, clamp, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.FourierPtr(ModulationPtr* p, uint len, bool clamp, float? scaleFactor, byte offset, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierExact(p, len, clamp, scaleFactor ?? float.NaN, offset, loopBehavior).Validate();

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
            Angle phase, bool clamp, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineExactFloat(Freq.Hz, config.Inner, intensity, offset, phase.Radian, clamp, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.FourierPtr(ModulationPtr* p, uint len, bool clamp, float? scaleFactor, byte offset, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierExactFloat(p, len, clamp, scaleFactor ?? float.NaN, offset, loopBehavior).Validate();
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
            Angle phase, bool clamp, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineNearest(Freq.Hz, config.Inner, intensity, offset, phase.Radian, clamp, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.FourierPtr(ModulationPtr* p, uint len, bool clamp, float? scaleFactor, byte offset, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierNearest(p, len, clamp, scaleFactor ?? float.NaN, offset, loopBehavior).Validate();
        ModulationPtr ISamplingMode.SquarePtr(SamplingConfig config, byte low, byte high,
            float duty, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSquareNearest(Freq.Hz, config.Inner, low, high, duty, loopBehavior).Validate();

        Freq<float> ISamplingMode.SineFreq(ModulationPtr ptr) => NativeMethodsBase.AUTDModulationSineNearestFreq(ptr) * Hz;
        Freq<float> ISamplingMode.SquareFreq(ModulationPtr ptr) => NativeMethodsBase.AUTDModulationSquareNearestFreq(ptr) * Hz;
    }

}