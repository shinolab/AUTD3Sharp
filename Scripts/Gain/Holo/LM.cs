using System;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Derive;
using AUTD3Sharp.Utils;
using System.Collections.Generic;

namespace AUTD3Sharp.Gain.Holo
{
    [Gain]
    [Builder]
    public sealed partial class LM : Holo<LM>
    {
        private readonly Backend _backend;
        private float[] _initial;

        public LM(Backend backend, IEnumerable<(Point3, Amplitude)> iter) : base(EmissionConstraint.Clamp(0x00, 0xFF), iter)
        {
            _backend = backend;
            Eps1 = 1e-8f;
            Eps2 = 1e-8f;
            Tau = 1e-3f;
            KMax = 5;
            _initial = Array.Empty<float>();
        }

        public LM WithInitial(float[] value)
        {
            _initial = value;
            return this;
        }

        [Property]
        public float Eps1 { get; private set; }

        [Property]
        public float Eps2 { get; private set; }

        [Property]
        public float Tau { get; private set; }

        [Property]
        public uint KMax { get; private set; }

        public ReadOnlySpan<float> Initial => new(_initial);

        private GainPtr GainPtr(Geometry _) =>
            _backend.Lm(Foci, Amps,
                (uint)Amps.Length, Eps1, Eps2, Tau, KMax, _initial, Constraint);
    }
}
