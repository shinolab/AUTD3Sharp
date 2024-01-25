using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram
{
    [ComVisible(false)]
    public abstract class Modulation : IDatagram
    {
        public SamplingConfiguration SamplingConfiguration => new SamplingConfiguration(NativeMethodsBase.AUTDModulationSamplingConfig(ModulationPtr()));

        DatagramPtr IDatagram.Ptr(Geometry.Geometry _) => NativeMethodsBase.AUTDModulationIntoDatagram(ModulationPtr());

        internal abstract ModulationPtr ModulationPtr();

        public int Length => NativeMethodsBase.AUTDModulationSize(ModulationPtr()).Validate();
    }

    public abstract class ModulationWithSamplingConfig<T> : Modulation
        where T : ModulationWithSamplingConfig<T>
    {
        protected SamplingConfiguration Config;

        protected ModulationWithSamplingConfig(SamplingConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// Set sampling frequency division
        /// </summary>
        /// <param name="config">Sampling configuration.</param>
        /// <returns></returns>
        public T WithSamplingConfig(SamplingConfiguration config)
        {
            Config = config;
            return (T)this;
        }
    }
}
