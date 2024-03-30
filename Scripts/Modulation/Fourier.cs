using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    /// <summary>
    /// Multi-frequency sine wave modulation
    /// </summary>
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Fourier
    {
        private readonly List<Sine> _components;

        /// <summary>
        /// Constructor
        /// </summary>
        public Fourier(Sine sine)
        {
            _components = new List<Sine> { sine };
        }

        public Fourier AddComponent(Sine sine)
        {
            _components.Add(sine);
            return this;
        }

        public Fourier AddComponentsFromIter(IEnumerable<Sine> iter)
        {
            return iter.Aggregate(this, (fourier, sine) => fourier.AddComponent(sine));
        }

        public static Fourier operator +(Fourier a, Sine b)
            => a.AddComponent(b);

        private ModulationPtr ModulationPtr()
        {
            var components = _components.Select(m => ((AUTD3Sharp.Driver.Datagram.Modulation.IModulation)m).ModulationPtr()).ToArray();
            unsafe
            {
                fixed (ModulationPtr* p = &components[0])
                    return NativeMethodsBase.AUTDModulationFourier(p, (uint)components.Length, LoopBehavior.Internal);
            }
        }
    }
}
