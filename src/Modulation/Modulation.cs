using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Base class for custom modulation
    /// </summary>
    public abstract class Modulation : Driver.Datagram.ModulationWithSamplingConfig<Modulation>
    {
        protected Modulation(SamplingConfiguration config) : base(config)
        {
        }

        internal sealed override ModulationPtr ModulationPtr()
        {
            var data = Calc();
            unsafe
            {
                fixed (EmitIntensity* ptr = &data[0])
                    return NativeMethodsBase.AUTDModulationCustom(Config.Internal, (byte*)ptr, (ulong)data.Length);
            }
        }

        public abstract EmitIntensity[] Calc();
    }
}
