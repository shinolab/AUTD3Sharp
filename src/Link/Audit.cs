using AUTD3Sharp.NativeMethods;
using System;
using AUTD3Sharp.Driver;

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

        public bool IsForceFan(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaIsForceFan(_ptr, (uint)idx);
        public void BreakDown() => NativeMethodsBase.AUTDLinkAuditBreakDown(_ptr);
        public ushort SilencerUpdateRateIntensity(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerUpdateRateIntensity(_ptr, (uint)idx);
        public ushort SilencerUpdateRatePhase(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerUpdateRatePhase(_ptr, (uint)idx);
        public ushort SilencerCompletionStepsIntensity(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerCompletionStepsIntensity(_ptr, (uint)idx);
        public ushort SilencerCompletionStepsPhase(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerCompletionStepsPhase(_ptr, (uint)idx);
        public bool SilencerFixedCompletionStepsMode(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerFixedCompletionStepsMode(_ptr, (uint)idx);
        public byte[] DebugTypes(int idx)
        {
            var ty = new byte[4];
            unsafe
            {
                fixed (byte* p = &ty[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaDebugTypes(_ptr, (uint)idx, p);
            }
            return ty;
        }

        public ushort[] DebugValues(int idx)
        {
            var value = new ushort[4];
            unsafe
            {
                fixed (ushort* p = &value[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaDebugValues(_ptr, (uint)idx, p);
            }
            return value;
        }

        public void AssertThermalSensor(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaAssertThermalSensor(_ptr, (uint)idx);

        public void DeassertThermalSensor(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaDeassertThermalSensor(_ptr, (uint)idx);

        public byte[] PhaseFilter(int idx)
        {
            var n = (int)NativeMethodsBase.AUTDLinkAuditCpuNumTransducers(_ptr, (uint)idx);
            var buf = new byte[n];
            unsafe
            {
                fixed (byte* p = &buf[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaPhaseFilter(_ptr, (uint)idx, p);
            }
            return buf;
        }

        public byte[] Modulation(int idx, Segment segment)
        {
            var n = (int)NativeMethodsBase.AUTDLinkAuditFpgaModulationCycle(_ptr, segment, (uint)idx);
            var buf = new byte[n];
            unsafe
            {
                fixed (byte* p = &buf[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaModulation(_ptr, segment, (uint)idx, p);
            }
            return buf;
        }

        public uint ModulationFreqDivision(int idx, Segment segment)=>NativeMethodsBase.AUTDLinkAuditFpgaModulationFreqDivision(_ptr, segment, (uint)idx);

        public NativeMethods.LoopBehavior ModulationLoopBehavior(int idx, Segment segment) =>
            NativeMethodsBase.AUTDLinkAuditFpgaModulationLoopBehavior(_ptr, segment, (uint)idx);

        public Segment CurrentModulationSegment(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaCurrentModSegment(_ptr, (uint)idx);

        public (byte[], byte[]) Drives(int idx, Segment segment, int stmIdx)
        {
            var n = (int)NativeMethodsBase.AUTDLinkAuditCpuNumTransducers(_ptr, (uint)idx);
            var intensities = new byte[n];
            var phases = new byte[n];
            unsafe
            {
                fixed (byte* pd = &intensities[0])
                fixed (byte* pp = &phases[0])
                {
                    NativeMethodsBase.AUTDLinkAuditFpgaDrives(_ptr, segment, (uint)idx, (uint)stmIdx, pd, pp);
                }
            }
            return (intensities, phases);
        }

        public uint StmCycle(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmCycle(_ptr, segment, (uint)idx);
        public bool IsStmGainMode(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaIsStmGainMode(_ptr, segment, (uint)idx);
        public uint StmFreqDivision(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmFreqDivision(_ptr, segment, (uint)idx);
        public uint StmSoundSpeed(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaSoundSpeed(_ptr, segment, (uint)idx);
        public NativeMethods.LoopBehavior StmLoopBehavior(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmLoopBehavior(_ptr, segment, (uint)idx);
        public Segment CurrentStmSegment(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaCurrentStmSegment(_ptr, (uint)idx);
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
