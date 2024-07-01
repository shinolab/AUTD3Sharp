using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;
using System.Collections.Generic;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class GSPAT : Holo<GSPAT>
    {
        private readonly Backend _backend;

        public GSPAT(Backend backend, IEnumerable<(Vector3, Amplitude)> iter) : base(EmissionConstraint.Clamp(0x00, 0xFF), iter)
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
