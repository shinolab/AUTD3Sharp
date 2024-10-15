using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [Modulation(ConfigNoChange = true)]
    [Builder]
    public sealed partial class Fourier
    {
        private readonly Sine[] _components;

        public Fourier(IEnumerable<Sine> iter)
        {
            _components = iter as Sine[] ?? iter.ToArray();
            Clamp = false;
            ScaleFactor = null;
        }

        [Property]
        public bool Clamp { get; private set; }

        [Property]
        public float? ScaleFactor { get; private set; }

        [Property]
        public byte Offset { get; private set; }

        private ModulationPtr ModulationPtr()
        {
            var components = _components.Select(m => ((Driver.Datagram.Modulation.IModulation)m).ModulationPtr()).ToArray();
            unsafe
            {
                fixed (ModulationPtr* p = &components[0])
                    return _components[0].Mode.FourierPtr(p, (uint)components.Length, Clamp, ScaleFactor, Offset, LoopBehavior);
            }
        }
    }
}
