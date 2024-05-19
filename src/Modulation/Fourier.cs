using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Driver.Datagram.Modulation;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Fourier
    {
        private readonly List<Sine> _components;

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

        private ModulationPtr ModulationPtr(Geometry geometry)
        {
            var components = _components.Select(m => ((AUTD3Sharp.Driver.Datagram.Modulation.IModulation)m).ModulationPtr(geometry)).ToArray();
            unsafe
            {
                fixed (ModulationPtr* p = &components[0])
                    return _components[0].Mode.FourierPtr(p, (uint)components.Length, LoopBehavior);
            }
        }
    }
}
