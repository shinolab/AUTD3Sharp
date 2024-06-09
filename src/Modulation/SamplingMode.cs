using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    internal interface ISamplingMode
    {
        internal ModulationPtr SinePtr(SamplingConfigWrap config, EmitIntensity intensity, EmitIntensity offset,
                                         Angle phase, NativeMethods.LoopBehavior loopBehavior);
        internal unsafe ModulationPtr FourierPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior);
        internal unsafe ModulationPtr MixerPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior);
        internal ModulationPtr SquarePtr(SamplingConfigWrap config, EmitIntensity low, EmitIntensity high,
                                           float duty, NativeMethods.LoopBehavior loopBehavior);
    }

    internal sealed class SamplingModeExact : ISamplingMode
    {
        internal SamplingModeExact(Freq<uint> freq)
        {
            Freq = freq;
        }

        internal Freq<uint> Freq { get; }

        ModulationPtr ISamplingMode.SinePtr(SamplingConfigWrap config, EmitIntensity intensity, EmitIntensity offset,
            Angle phase, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineExact(Freq.Hz, config, intensity.Value, offset.Value, phase.Radian, loopBehavior);

        unsafe ModulationPtr ISamplingMode.FourierPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierExact(p, len, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.MixerPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierExact(p, len, loopBehavior).Validate();

        ModulationPtr ISamplingMode.SquarePtr(SamplingConfigWrap config, EmitIntensity low, EmitIntensity high,
            float duty, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSquareExact(Freq.Hz, config, low.Value, high.Value, duty, loopBehavior);
    }

    internal sealed class SamplingModeExactFloat : ISamplingMode
    {
        internal SamplingModeExactFloat(Freq<float> freq)
        {
            Freq = freq;
        }

        internal Freq<float> Freq { get; }

        ModulationPtr ISamplingMode.SinePtr(SamplingConfigWrap config, EmitIntensity intensity, EmitIntensity offset,
            Angle phase, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineExactFloat(Freq.Hz, config, intensity.Value, offset.Value, phase.Radian, loopBehavior);

        unsafe ModulationPtr ISamplingMode.FourierPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierExactFloat(p, len, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.MixerPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierExactFloat(p, len, loopBehavior).Validate();

        ModulationPtr ISamplingMode.SquarePtr(SamplingConfigWrap config, EmitIntensity low, EmitIntensity high,
            float duty, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSquareExactFloat(Freq.Hz, config, low.Value, high.Value, duty, loopBehavior);
    }

    internal sealed class SamplingModeNearest : ISamplingMode
    {
        internal SamplingModeNearest(Freq<float> freq)
        {
            Freq = freq;
        }

        internal Freq<float> Freq { get; }

        ModulationPtr ISamplingMode.SinePtr(SamplingConfigWrap config, EmitIntensity intensity, EmitIntensity offset,
            Angle phase, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineNearest(Freq.Hz, config, intensity.Value, offset.Value, phase.Radian, loopBehavior);

        unsafe ModulationPtr ISamplingMode.FourierPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierNearest(p, len, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.MixerPtr(ModulationPtr* p, uint len, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationFourierNearest(p, len, loopBehavior).Validate();

        ModulationPtr ISamplingMode.SquarePtr(SamplingConfigWrap config, EmitIntensity low, EmitIntensity high,
            float duty, NativeMethods.LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSquareNearest(Freq.Hz, config, low.Value, high.Value, duty, loopBehavior);
    }

}