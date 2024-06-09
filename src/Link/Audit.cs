using AUTD3Sharp.NativeMethods;
using System;
using AUTD3Sharp.Driver;
using System.Linq;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Link
{
    public sealed class Audit
    {
        public sealed class AuditBuilder : ILinkBuilder<Audit>
        {
            private LinkAuditBuilderPtr _ptr;

            internal AuditBuilder()
            {
                _ptr = NativeMethodsBase.AUTDLinkAudit();
            }

            public AuditBuilder WithTimeout(TimeSpan timeout)
            {
                _ptr = NativeMethodsBase.AUTDLinkAuditWithTimeout(_ptr, (ulong)(timeout.TotalMilliseconds * 1000 * 1000));
                return this;
            }

            LinkBuilderPtr ILinkBuilder<Audit>.Ptr()
            {
                return NativeMethodsBase.AUTDLinkAuditIntoBuilder(_ptr);
            }

            Audit ILinkBuilder<Audit>.ResolveLink(LinkPtr ptr)
            {
                return new Audit
                {
                    _ptr = ptr
                };
            }
        }

        private LinkPtr _ptr = new LinkPtr { Item1 = IntPtr.Zero };

        public static AuditBuilder Builder()
        {
            return new AuditBuilder();
        }

        public TimeSpan Timeout() => TimeSpan.FromSeconds(NativeMethodsBase.AUTDLinkAuditTimeoutNs(_ptr) / 1000.0 / 1000.0 / 1000.0);

        public TimeSpan LastTimeout() => TimeSpan.FromSeconds(NativeMethodsBase.AUTDLinkAuditLastTimeoutNs(_ptr) / 1000.0 / 1000.0 / 1000.0);

        public void Down()
        {
            NativeMethodsBase.AUTDLinkAuditDown(_ptr);
        }

        public bool IsOpen()
        {
            return NativeMethodsBase.AUTDLinkAuditIsOpen(_ptr);
        }

        public bool IsForceFan(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaIsForceFan(_ptr, (ushort)idx);
        public void BreakDown() => NativeMethodsBase.AUTDLinkAuditBreakDown(_ptr);
        public ushort SilencerUpdateRateIntensity(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerUpdateRateIntensity(_ptr, (ushort)idx);
        public ushort SilencerUpdateRatePhase(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerUpdateRatePhase(_ptr, (ushort)idx);
        public ushort SilencerCompletionStepsIntensity(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerCompletionStepsIntensity(_ptr, (ushort)idx);
        public ushort SilencerCompletionStepsPhase(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerCompletionStepsPhase(_ptr, (ushort)idx);
        public bool SilencerFixedCompletionStepsMode(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerFixedCompletionStepsMode(_ptr, (ushort)idx);
        public byte[] DebugTypes(int idx)
        {
            var ty = new byte[4];
            unsafe
            {
                fixed (byte* p = &ty[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaDebugTypes(_ptr, (ushort)idx, p);
            }
            return ty;
        }

        public ushort[] DebugValues(int idx)
        {
            var value = new ushort[4];
            unsafe
            {
                fixed (ushort* p = &value[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaDebugValues(_ptr, (ushort)idx, p);
            }
            return value;
        }

        public void AssertThermalSensor(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaAssertThermalSensor(_ptr, (ushort)idx);

        public void DeassertThermalSensor(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaDeassertThermalSensor(_ptr, (ushort)idx);

        public byte[] Modulation(int idx, Segment segment)
        {
            var n = (int)NativeMethodsBase.AUTDLinkAuditFpgaModulationCycle(_ptr, segment, (ushort)idx);
            var buf = new byte[n];
            unsafe
            {
                fixed (byte* p = &buf[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaModulation(_ptr, segment, (ushort)idx, p);
            }
            return buf;
        }

        public uint ModulationFreqDivision(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaModulationFreqDivision(_ptr, segment, (ushort)idx);

        public NativeMethods.LoopBehavior ModulationLoopBehavior(int idx, Segment segment) =>
            NativeMethodsBase.AUTDLinkAuditFpgaModulationLoopBehavior(_ptr, segment, (ushort)idx);

        public Segment CurrentModulationSegment(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaCurrentModSegment(_ptr, (ushort)idx);

        public (byte[], byte[]) Drives(int idx, Segment segment, int stmIdx)
        {
            var n = (int)NativeMethodsBase.AUTDLinkAuditCpuNumTransducers(_ptr, (ushort)idx);
            var intensities = new byte[n];
            var phases = new byte[n];
            unsafe
            {
                fixed (byte* pd = &intensities[0])
                fixed (byte* pp = &phases[0])
                {
                    NativeMethodsBase.AUTDLinkAuditFpgaDrives(_ptr, segment, (ushort)idx, (ushort)stmIdx, pd, pp);
                }
            }
            return (intensities, phases);
        }

        public uint StmCycle(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmCycle(_ptr, segment, (ushort)idx);
        public bool IsStmGainMode(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaIsStmGainMode(_ptr, segment, (ushort)idx);
        public uint StmFreqDivision(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmFreqDivision(_ptr, segment, (ushort)idx);
        public uint StmSoundSpeed(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaSoundSpeed(_ptr, segment, (ushort)idx);
        public NativeMethods.LoopBehavior StmLoopBehavior(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmLoopBehavior(_ptr, segment, (ushort)idx);
        public Segment CurrentStmSegment(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaCurrentStmSegment(_ptr, (ushort)idx);
        public uint UltrasoundFreq(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaUltrasoundFreq(_ptr, (ushort)idx);

        public ushort[] PulseWidthEncoderTable(int idx)
        {
            var table = new ushort[65536];
            unsafe
            {
                var buf = new byte[65536];
                fixed (byte* p = &buf[0])
                {
                    var fullWidthStart = NativeMethodsBase.AUTDLinkAuditFpgaPulseWidthEncoderTable(_ptr, (ushort)idx, p);
                    Enumerable.Range(0, 65536).ToList().ForEach(i => table[i] = i < fullWidthStart ? buf[i] : (ushort)(0x100 | buf[i]));
                }
            }
            return table;
        }

    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
