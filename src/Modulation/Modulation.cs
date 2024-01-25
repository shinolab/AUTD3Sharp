using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Base class for custom modulation
    /// </summary>
    public abstract class Modulation : Internal.Modulation
    {
        private readonly SamplingConfiguration _config;

        protected Modulation(SamplingConfiguration config)
        {
            _config = config;
        }

        internal sealed override ModulationPtr ModulationPtr()
        {
            var data = Calc();
            unsafe
            {
                fixed (EmitIntensity* ptr = &data[0])
                    return NativeMethodsBase.AUTDModulationCustom(_config.Internal, (byte*)ptr, (ulong)data.Length);
            }
        }

        public abstract EmitIntensity[] Calc();
    }
}
