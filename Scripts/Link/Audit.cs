using System.Linq;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Link
{
    public sealed class Audit : Driver.Link, Driver.ILink
    {
        public bool IsOpen() => NativeMethodsBase.AUTDLinkAuditIsOpen(Ptr);
        public bool IsForceFan(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaIsForceFan(Ptr, (ushort)idx);
        public void BreakDown() => NativeMethodsBase.AUTDLinkAuditBreakDown(Ptr);
        public void Repair() => NativeMethodsBase.AUTDLinkAuditRepair(Ptr);

        public ushort SilencerUpdateRateIntensity(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerUpdateRateIntensity(Ptr, (ushort)idx);
        public ushort SilencerUpdateRatePhase(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerUpdateRatePhase(Ptr, (ushort)idx);
        public ushort SilencerCompletionStepsIntensity(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerCompletionStepsIntensity(Ptr, (ushort)idx);
        public ushort SilencerCompletionStepsPhase(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerCompletionStepsPhase(Ptr, (ushort)idx);
        public bool SilencerFixedCompletionStepsMode(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaSilencerFixedCompletionStepsMode(Ptr, (ushort)idx);
        public bool SilencerStrictMode(int idx) => NativeMethodsBase.AUTDLinkAuditCpuSilencerStrictMode(Ptr, (ushort)idx);

        public byte[] GPIOOutputTypes(int idx)
        {
            var ty = new byte[4];
            unsafe
            {
                fixed (byte* p = &ty[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaGPIOOutputTypes(Ptr, (ushort)idx, p);
            }
            return ty;
        }

        public ulong[] DebugValues(int idx)
        {
            var value = new ulong[4];
            unsafe
            {
                fixed (ulong* p = &value[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaDebugValues(Ptr, (ushort)idx, p);
            }
            return value;
        }

        public void AssertThermalSensor(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaAssertThermalSensor(Ptr, (ushort)idx);

        public void DeassertThermalSensor(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaDeassertThermalSensor(Ptr, (ushort)idx);

        public byte[] Modulation(int idx, Segment segment)
        {
            var n = NativeMethodsBase.AUTDLinkAuditFpgaModulationCycle(Ptr, segment.ToNative(), (ushort)idx);
            var buf = new byte[n];
            unsafe
            {
                fixed (byte* p = &buf[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaModulationBuffer(Ptr, segment.ToNative(), (ushort)idx, p, n);
            }
            return buf;
        }

        public ushort ModulationFreqDivision(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaModulationFreqDivision(Ptr, segment.ToNative(), (ushort)idx);

        public LoopBehavior ModulationLoopBehavior(int idx, Segment segment) =>
            new(NativeMethodsBase.AUTDLinkAuditFpgaModulationLoopBehavior(Ptr, segment.ToNative(), (ushort)idx));

        public Segment CurrentModulationSegment(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaCurrentModSegment(Ptr, (ushort)idx).ToManaged();

        public (byte[], byte[]) Drives(int idx, Segment segment, int stmIdx)
        {
            var n = (int)NativeMethodsBase.AUTDLinkAuditCpuNumTransducers(Ptr, (ushort)idx);
            var drive = new NativeMethods.Drive[n];
            unsafe
            {
                fixed (NativeMethods.Drive* pd = &drive[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaDrivesAt(Ptr, segment.ToNative(), (ushort)idx, (ushort)stmIdx, pd);
            }
            var intensities = drive.Select(d => d.intensity.Item1).ToArray();
            var phases = drive.Select(d => d.phase.Item1).ToArray();
            return (intensities, phases);
        }

        public ushort StmCycle(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmCycle(Ptr, segment.ToNative(), (ushort)idx);
        public bool IsStmGainMode(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaIsStmGainMode(Ptr, segment.ToNative(), (ushort)idx);
        public ushort StmFreqDivision(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaStmFreqDivision(Ptr, segment.ToNative(), (ushort)idx);
        public ushort StmSoundSpeed(int idx, Segment segment) => NativeMethodsBase.AUTDLinkAuditFpgaSoundSpeed(Ptr, segment.ToNative(), (ushort)idx);
        public LoopBehavior StmLoopBehavior(int idx, Segment segment) => new(NativeMethodsBase.AUTDLinkAuditFpgaStmLoopBehavior(Ptr, segment.ToNative(), (ushort)idx));
        public Segment CurrentStmSegment(int idx) => NativeMethodsBase.AUTDLinkAuditFpgaCurrentStmSegment(Ptr, (ushort)idx).ToManaged();

        public ushort[] PulseWidthEncoderTable(int idx)
        {
            var table = new ushort[256];
            unsafe
            {
                fixed (ushort* p = &table[0])
                    NativeMethodsBase.AUTDLinkAuditFpgaPulseWidthEncoderTable(Ptr, (ushort)idx, p);
            }
            return table;
        }

        public override LinkPtr Resolve() => NativeMethodsBase.AUTDLinkAudit();

        public Audit() { }

        void Driver.ILink.Resolve(LinkPtr ptr) => Ptr = ptr;
    }
}
