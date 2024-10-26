using System;
using System.Linq;
using AUTD3Sharp.NativeMethods;
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
            internal AuditBuilder()
            {
            }

            LinkBuilderPtr ILinkBuilder<Audit>.Ptr()
            {
                return NativeMethodsBase.AUTDLinkAudit();
            }

            Audit ILinkBuilder<Audit>.ResolveLink(RuntimePtr _, LinkPtr ptr)
            {
                return new Audit
                {
                    _ptr = ptr
                };
            }
        }

        private LinkPtr _ptr = new() { Item1 = IntPtr.Zero };

        public static AuditBuilder Builder()
        {
            return new AuditBuilder();
        }

        public void Down() => NativeMethodsBase.AUTDLinkAuditDown(_ptr);
        public void Up() => NativeMethodsBase.AUTDLinkAuditUp(_ptr);
        public bool IsOpen() => NativeMethodsBase.AUTDLinkAuditIsOpen(_ptr);
        public bool IsForceFan(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaIsForceFan(_ptr, (ushort)idx);
        public void BreakDown() => NativeMethodsBase.AUTDLinkAuditBreakDown(_ptr);
        public void Repair() => NativeMethodsBase.AUTDLinkAuditRepair(_ptr);
        public ushort SilencerUpdateRateIntensity(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerUpdateRateIntensity(_ptr, (ushort)idx);
        public ushort SilencerUpdateRatePhase(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerUpdateRatePhase(_ptr, (ushort)idx);
        public ushort SilencerCompletionStepsIntensity(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerCompletionStepsIntensity(_ptr, (ushort)idx);
        public ushort SilencerCompletionStepsPhase(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerCompletionStepsPhase(_ptr, (ushort)idx);
        public bool SilencerFixedCompletionStepsMode(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerFixedCompletionStepsMode(_ptr, (ushort)idx);
        public bool SilencerStrictMode(int idx) => NativeMethodsBase.AUTDLinkAuditCpuSilencerStrictMode(_ptr, (ushort)idx);

        public SilencerTarget SilencerTarget(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerTarget(_ptr, (ushort)idx);

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

        public ulong[] DebugValues(int idx)
        {
            var value = new ulong[4];
            unsafe
            {
                fixed (ulong* p = &value[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaDebugValues(_ptr, (ushort)idx, p);
            }
            return value;
        }

        public void AssertThermalSensor(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaAssertThermalSensor(_ptr, (ushort)idx);

        public void DeassertThermalSensor(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaDeassertThermalSensor(_ptr, (ushort)idx);

        public byte[] Modulation(int idx, Segment segment)
        {
            var n = NativeMethodsBase.AUTDLinkAuditFpgaModulationCycle(_ptr, segment, (ushort)idx);
            var buf = new byte[n];
            unsafe
            {
                fixed (byte* p = &buf[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaModulationBuffer(_ptr, segment, (ushort)idx, p, n);
            }
            return buf;
        }

        public ushort ModulationFreqDivision(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaModulationFreqDivision(_ptr, segment, (ushort)idx);

        public LoopBehavior ModulationLoopBehavior(int idx, Segment segment) =>
            NativeMethodsBase.AUTDLinkAuditFpgaModulationLoopBehavior(_ptr, segment, (ushort)idx);

        public Segment CurrentModulationSegment(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaCurrentModSegment(_ptr, (ushort)idx);

        public (byte[], byte[]) Drives(int idx, Segment segment, int stmIdx)
        {
            var n = (int)NativeMethodsBase.AUTDLinkAuditCpuNumTransducers(_ptr, (ushort)idx);
            var drive = new Drive[n];
            unsafe
            {
                fixed (Drive* pd = &drive[0])
                {
                    NativeMethodsBase.AUTDLinkAuditFpgaDrivesAt(_ptr, segment, (ushort)idx, (ushort)stmIdx, (Drive*)pd);
                }
            }
            var intensities = drive.Select(d => d.Intensity.Value).ToArray();
            var phases = drive.Select(d => d.Phase.Value).ToArray();
            return (intensities, phases);
        }

        public ushort StmCycle(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmCycle(_ptr, segment, (ushort)idx);
        public bool IsStmGainMode(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaIsStmGainMode(_ptr, segment, (ushort)idx);
        public ushort StmFreqDivision(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmFreqDivision(_ptr, segment, (ushort)idx);
        public ushort StmSoundSpeed(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaSoundSpeed(_ptr, segment, (ushort)idx);
        public LoopBehavior StmLoopBehavior(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmLoopBehavior(_ptr, segment, (ushort)idx);
        public Segment CurrentStmSegment(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaCurrentStmSegment(_ptr, (ushort)idx);

        public byte[] PulseWidthEncoderTable(int idx)
        {
            var table = new byte[256];
            unsafe
            {
                fixed (byte* p = &table[0])
                {
                    NativeMethodsBase.AUTDLinkAuditFpgaPulseWidthEncoderTable(_ptr, (ushort)idx, p);
                }
            }
            return table;
        }

    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
