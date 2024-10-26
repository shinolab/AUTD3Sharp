using System.Linq;
using AUTD3Sharp.NativeMethods;
using static AUTD3Sharp.Units;

namespace AUTD3Sharp.Modulation
{
    internal interface ISamplingMode
    {
        internal ModulationPtr SinePtr(SamplingConfig config, byte intensity, byte offset,
                                         Angle phase, bool clamp, LoopBehavior loopBehavior);
        internal ModulationPtr FourierPtr(Sine[] components, bool clamp, float? scaleFactor, byte offset, LoopBehavior loopBehavior);
        internal ModulationPtr SquarePtr(SamplingConfig config, byte low, byte high,
                                           float duty, LoopBehavior loopBehavior);
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
            Angle phase, bool clamp, LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineExact(Freq.Hz, config, intensity, offset, phase.Radian, clamp, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.FourierPtr(Sine[] components, bool clamp, float? scaleFactor,
            byte offset, LoopBehavior loopBehavior)
        {
            var sineFreq = components.Select(s => ((SamplingModeExact)s.Mode).Freq.Hz).ToArray();
            var sineConfig = components.Select(s => s.SamplingConfig).ToArray();
            var sineIntensity = components.Select(s => s.Intensity).ToArray();
            var sineOffset = components.Select(s => s.Offset).ToArray();
            var sinePhase = components.Select(s => s.Phase.Radian).ToArray();
            var sineClamp = components.Select(s => s.Clamp).ToArray();
            fixed (uint* pFreq = &sineFreq[0])
            fixed (SamplingConfig* pConfig = &sineConfig[0])
            fixed (byte* pIntensity = &sineIntensity[0])
            fixed (byte* pOffset = &sineOffset[0])
            fixed (float* pPhase = &sinePhase[0])
            fixed (bool* pClamp = &sineClamp[0])
            {
                return NativeMethodsBase.AUTDModulationFourierExact(pFreq, pConfig, pIntensity, pOffset, pPhase, pClamp,
                    (uint)components.Length, clamp, scaleFactor ?? float.NaN, offset, loopBehavior).Validate();
            }
        }
        ModulationPtr ISamplingMode.SquarePtr(SamplingConfig config, byte low, byte high,
            float duty, LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSquareExact(Freq.Hz, config, low, high, duty, loopBehavior).Validate();

        Freq<float> ISamplingMode.SineFreq(ModulationPtr ptr) => (float)NativeMethodsBase.AUTDModulationSineExactFreq(Freq.Hz) * Hz;
        Freq<float> ISamplingMode.SquareFreq(ModulationPtr ptr) => (float)NativeMethodsBase.AUTDModulationSquareExactFreq(Freq.Hz) * Hz;
    }

    internal sealed class SamplingModeExactFloat : ISamplingMode
    {
        internal SamplingModeExactFloat(Freq<float> freq)
        {
            Freq = freq;
        }

        internal Freq<float> Freq { get; }

        ModulationPtr ISamplingMode.SinePtr(SamplingConfig config, byte intensity, byte offset,
            Angle phase, bool clamp, LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineExactFloat(Freq.Hz, config, intensity, offset, phase.Radian, clamp, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.FourierPtr(Sine[] components, bool clamp, float? scaleFactor,
            byte offset, LoopBehavior loopBehavior)
        {
            var sineFreq = components.Select(s => ((SamplingModeExactFloat)s.Mode).Freq.Hz).ToArray();
            var sineConfig = components.Select(s => s.SamplingConfig).ToArray();
            var sineIntensity = components.Select(s => s.Intensity).ToArray();
            var sineOffset = components.Select(s => s.Offset).ToArray();
            var sinePhase = components.Select(s => s.Phase.Radian).ToArray();
            var sineClamp = components.Select(s => s.Clamp).ToArray();
            fixed (float* pFreq = &sineFreq[0])
            fixed (SamplingConfig* pConfig = &sineConfig[0])
            fixed (byte* pIntensity = &sineIntensity[0])
            fixed (byte* pOffset = &sineOffset[0])
            fixed (float* pPhase = &sinePhase[0])
            fixed (bool* pClamp = &sineClamp[0])
            {
                return NativeMethodsBase.AUTDModulationFourierExactFloat(pFreq, pConfig, pIntensity, pOffset, pPhase, pClamp,
                    (uint)components.Length, clamp, scaleFactor ?? float.NaN, offset, loopBehavior).Validate();
            }
        }
        ModulationPtr ISamplingMode.SquarePtr(SamplingConfig config, byte low, byte high,
            float duty, LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSquareExactFloat(Freq.Hz, config, low, high, duty, loopBehavior).Validate();

        Freq<float> ISamplingMode.SineFreq(ModulationPtr ptr) => NativeMethodsBase.AUTDModulationSineExactFloatFreq(Freq.Hz) * Hz;
        Freq<float> ISamplingMode.SquareFreq(ModulationPtr ptr) => NativeMethodsBase.AUTDModulationSquareExactFloatFreq(Freq.Hz) * Hz;
    }

    internal sealed class SamplingModeNearest : ISamplingMode
    {
        internal SamplingModeNearest(Freq<float> freq)
        {
            Freq = freq;
        }

        internal Freq<float> Freq { get; }

        ModulationPtr ISamplingMode.SinePtr(SamplingConfig config, byte intensity, byte offset,
            Angle phase, bool clamp, LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSineNearest(Freq.Hz, config, intensity, offset, phase.Radian, clamp, loopBehavior).Validate();

        unsafe ModulationPtr ISamplingMode.FourierPtr(Sine[] components, bool clamp, float? scaleFactor,
            byte offset, LoopBehavior loopBehavior)
        {
            var sineFreq = components.Select(s => ((SamplingModeNearest)s.Mode).Freq.Hz).ToArray();
            var sineConfig = components.Select(s => s.SamplingConfig).ToArray();
            var sineIntensity = components.Select(s => s.Intensity).ToArray();
            var sineOffset = components.Select(s => s.Offset).ToArray();
            var sinePhase = components.Select(s => s.Phase.Radian).ToArray();
            var sineClamp = components.Select(s => s.Clamp).ToArray();
            fixed (float* pFreq = &sineFreq[0])
            fixed (SamplingConfig* pConfig = &sineConfig[0])
            fixed (byte* pIntensity = &sineIntensity[0])
            fixed (byte* pOffset = &sineOffset[0])
            fixed (float* pPhase = &sinePhase[0])
            fixed (bool* pClamp = &sineClamp[0])
            {
                return NativeMethodsBase.AUTDModulationFourierNearest(pFreq, pConfig, pIntensity, pOffset, pPhase, pClamp,
                    (uint)components.Length, clamp, scaleFactor ?? float.NaN, offset, loopBehavior).Validate();
            }
        }
        ModulationPtr ISamplingMode.SquarePtr(SamplingConfig config, byte low, byte high,
            float duty, LoopBehavior loopBehavior) => NativeMethodsBase.AUTDModulationSquareNearest(Freq.Hz, config, low, high, duty, loopBehavior).Validate();

        Freq<float> ISamplingMode.SineFreq(ModulationPtr ptr) => NativeMethodsBase.AUTDModulationSineNearestFreq(Freq.Hz) * Hz;
        Freq<float> ISamplingMode.SquareFreq(ModulationPtr ptr) => NativeMethodsBase.AUTDModulationSquareNearestFreq(Freq.Hz) * Hz;
    }

}