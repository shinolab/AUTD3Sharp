using System;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class LM<TB> : Holo<LM<TB>>
        where TB : Backend
    {
        private readonly TB _backend;
        private double[] _initial;

        public LM(TB backend) : base(EmissionConstraint.DontCare)
        {
            _backend = backend;
            Eps1 = 1e-8;
            Eps2 = 1e-8;
            Tau = 1e-3;
            KMax = 5;
            _initial = Array.Empty<double>();
        }

        public LM<TB> WithInitial(double[] value)
        {
            _initial = value;
            return this;
        }

        [Property]
        public double Eps1 { get; private set; }

        [Property]
        public double Eps2 { get; private set; }

        [Property]
        public double Tau { get; private set; }

        [Property]
        public uint KMax { get; private set; }

        public ReadOnlySpan<double> Initial => new ReadOnlySpan<double>(_initial);

        private GainPtr GainPtr(Geometry _) =>
            _backend.Lm(Foci.ToArray(), Amps.ToArray(),
                (ulong)Amps.Count, Eps1, Eps2, Tau, KMax, _initial, Constraint);
    }
}
