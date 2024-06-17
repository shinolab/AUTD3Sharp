using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;
using System.Collections.Generic;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class SDP<TB> : Holo<SDP<TB>>
           where TB : Backend
    {
        private readonly TB _backend;

        public SDP(TB backend, IEnumerable<(Vector3, Amplitude)> iter) : base(EmissionConstraint.DontCare, iter)
        {
            _backend = backend;
            Alpha = 1e-3f;
            Lambda = 0.9f;
            Repeat = 100;
        }

        [Property]
        public float Alpha { get; private set; }

        [Property]
        public float Lambda { get; private set; }

        [Property]
        public uint Repeat { get; private set; }

        private GainPtr GainPtr(Geometry _) =>
            _backend.Sdp(Foci, Amps, (uint)Amps.Length, Alpha, Repeat, Lambda, Constraint);
    }
}
