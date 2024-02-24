using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Base class for custom modulation
    /// </summary>
    public abstract class Modulation : Driver.Datagram.Modulation.Modulation
    {
        protected Modulation(SamplingConfiguration config) : base()
        {
            Config = config;
        }

        internal override ModulationPtr ModulationPtr()
        {
            var data = Calc();
            unsafe
            {
                fixed (EmitIntensity* ptr = &data[0])
                    return NativeMethodsBase.AUTDModulationCustom(Config.Internal, (byte*)ptr, (ulong)data.Length, LoopBehavior.Internal);
            }
        }

        public abstract EmitIntensity[] Calc();
    }
}
