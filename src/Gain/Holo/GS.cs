using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;
using AUTD3Sharp.Derive;
using System.Collections.Generic;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class GS<TB> : Holo<GS<TB>>
            where TB : Backend
    {
        private readonly TB _backend;

        public GS(TB backend, IEnumerable<(Vector3, Amplitude)> iter) : base(EmissionConstraint.DontCare, iter)
        {
            _backend = backend;
            Repeat = 100;
        }

        [Property] public uint Repeat { get; private set; }

        private GainPtr GainPtr(Geometry _)
        {
            return _backend.Gs(Foci, Amps, (uint)Amps.Length, Repeat, Constraint);
        }
    }
}
