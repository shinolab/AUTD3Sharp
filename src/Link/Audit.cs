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

        public void Down()
        {
            NativeMethodsBase.AUTDLinkAuditDown(_ptr);
        }

        public bool IsOpen()
        {
            return NativeMethodsBase.AUTDLinkAuditIsOpen(_ptr);
        }

        public bool IsForceFan(int idx)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaIsForceFan(_ptr, (uint)idx);
        }

        public void BreakDown()
        {
            NativeMethodsBase.AUTDLinkAuditBreakDown(_ptr);
        }

        public ushort SilencerUpdateRateIntensity(int idx)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaSilencerUpdateRateIntensity(_ptr, (uint)idx);
        }

        public ushort SilencerUpdateRatePhase(int idx)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaSilencerUpdateRatePhase(_ptr, (uint)idx);
        }

        public ushort SilencerCompletionStepsIntensity(int idx)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaSilencerCompletionStepsIntensity(_ptr, (uint)idx);
        }

        public ushort SilencerCompletionStepsPhase(int idx)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaSilencerCompletionStepsPhase(_ptr, (uint)idx);
        }

        public bool SilencerFixedCompletionStepsMode(int idx)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaSilencerFixedCompletionStepsMode(_ptr, (uint)idx);
        }

        public byte DebugOutputIdx(int idx)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaDebugOutputIdx(_ptr, (uint)idx);
        }

        public void AssertThermalSensor(int idx)
        {
            NativeMethodsBase.AUTDLinkAuditFpgaAssertThermalSensor(_ptr, (uint)idx);
        }

        public void DeassertThermalSensor(int idx)
        {
            NativeMethodsBase.AUTDLinkAuditFpgaDeassertThermalSensor(_ptr, (uint)idx);
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

        public uint ModulationFrequencyDivision(int idx, Segment segment)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaModulationFrequencyDivision(_ptr, segment,(uint)idx);
        }

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
                    NativeMethodsBase.AUTDLinkAuditFpgaDrives(_ptr, segment,(uint)idx, (uint)stmIdx, pd, pp);
                }
            }
            return (intensities, phases);
        }

        public uint StmCycle(int idx, Segment segment)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaStmCycle(_ptr, segment,(uint)idx);
        }

        public bool IsStmGainMode(int idx, Segment segment)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaIsStmGainMode(_ptr, segment,(uint)idx);
        }

        public uint StmFrequencyDivision(int idx, Segment segment)
        {
            return NativeMethodsBase.AUTDLinkAuditFpgaStmFrequencyDivision(_ptr, segment,(uint)idx);
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
