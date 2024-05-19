using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class SDP<TB> : Holo<SDP<TB>>
           where TB : Backend
    {
        private readonly TB _backend;

        public SDP(TB backend) : base(EmissionConstraint.DontCare)
        {
            _backend = backend;
            Alpha = 1e-3;
            Lambda = 0.9;
            Repeat = 100;
        }

        [Property]
        public double Alpha { get; private set; }

        [Property]
        public double Lambda { get; private set; }

        [Property]
        public uint Repeat { get; private set; }

        private GainPtr GainPtr(Geometry _) =>
            _backend.Sdp(Foci.ToArray(), Amps.ToArray(),
                (ulong)Amps.Count, Alpha, Repeat, Lambda, Constraint);
    }
}
