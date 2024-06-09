using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;
using System.Collections.Generic;
using System.Linq;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class GSPAT<TB> : Holo<GSPAT<TB>>
            where TB : Backend
    {
        private readonly TB _backend;

        public GSPAT(TB backend, IEnumerable<(Vector3, Amplitude)> iter) : base(EmissionConstraint.DontCare, iter)
        {
            _backend = backend;
            Repeat = 100;
        }

        [Property]

        public uint Repeat { get; private set; }

        private GainPtr GainPtr(Geometry _)
        {
            return _backend.Gspat(Foci, Amps, (uint)Amps.Length, Repeat, Constraint);
        }
    }
}
