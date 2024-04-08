// <auto-generated>
// This code is generated by csbindgen.
// DON'T CHANGE THIS DIRECTLY.
// </auto-generated>
#pragma warning disable CS8500
#pragma warning disable CS8981
using System;
using System.Runtime.InteropServices;


namespace AUTD3Sharp.NativeMethods
{
    public static unsafe partial class NativeMethodsBase
    {
        const string __DllName = "autd3capi";



        [DllImport(__DllName, EntryPoint = "AUTDDatagramClear", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDDatagramClear();

        [DllImport(__DllName, EntryPoint = "AUTDDatagramConfigureDebugSettings", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDDatagramConfigureDebugSettings(IntPtr f, IntPtr context, GeometryPtr geometry);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramConfigureForceFan", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDDatagramConfigureForceFan(IntPtr f, IntPtr context, GeometryPtr geometry);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramConfigurePhaseFilter", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDDatagramConfigurePhaseFilter(IntPtr f, IntPtr context, GeometryPtr geometry);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramConfigureReadsFPGAState", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDDatagramConfigureReadsFPGAState(IntPtr f, IntPtr context, GeometryPtr geometry);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramSilencerFixedUpdateRate", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultDatagram AUTDDatagramSilencerFixedUpdateRate(ushort value_intensity, ushort value_phase);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramSilencerFixedCompletionSteps", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultDatagram AUTDDatagramSilencerFixedCompletionSteps(ushort value_intensity, ushort value_phase, [MarshalAs(UnmanagedType.U1)] bool strict_mode);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramSilencerFixedCompletionStepsIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDDatagramSilencerFixedCompletionStepsIsDefault(DatagramPtr silencer);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramSynchronize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDDatagramSynchronize();

        [DllImport(__DllName, EntryPoint = "AUTDGainBessel", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainBessel(double x, double y, double z, double nx, double ny, double nz, double theta_z, byte intensity, byte phase_offset);

        [DllImport(__DllName, EntryPoint = "AUTDGainBesselIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDGainBesselIsDefault(GainPtr bessel);

        [DllImport(__DllName, EntryPoint = "AUTDGainCustom", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainCustom();

        [DllImport(__DllName, EntryPoint = "AUTDGainCustomSet", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainCustomSet(GainPtr custom, uint dev_idx, DriveRaw* ptr, uint len);

        [DllImport(__DllName, EntryPoint = "AUTDGainFocus", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainFocus(double x, double y, double z, byte intensity, byte phase_offset);

        [DllImport(__DllName, EntryPoint = "AUTDGainFocusIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDGainFocusIsDefault(GainPtr focus);

        [DllImport(__DllName, EntryPoint = "AUTDGainGroupCreateMap", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GroupGainMapPtr AUTDGainGroupCreateMap(uint* device_indices_ptr, uint num_devices);

        [DllImport(__DllName, EntryPoint = "AUTDGainGroupMapSet", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GroupGainMapPtr AUTDGainGroupMapSet(GroupGainMapPtr map, uint dev_idx, int* map_data);

        [DllImport(__DllName, EntryPoint = "AUTDGainGroup", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainGroup(GroupGainMapPtr map, int* keys_ptr, GainPtr* values_ptr, uint kv_len);

        [DllImport(__DllName, EntryPoint = "AUTDGainIntoDatagramWithSegment", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDGainIntoDatagramWithSegment(GainPtr gain, Segment segment, [MarshalAs(UnmanagedType.U1)] bool update_segment);

        [DllImport(__DllName, EntryPoint = "AUTDGainIntoDatagram", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDGainIntoDatagram(GainPtr gain);

        [DllImport(__DllName, EntryPoint = "AUTDGainCalc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultGainCalcDrivesMap AUTDGainCalc(GainPtr gain, GeometryPtr geometry);

        [DllImport(__DllName, EntryPoint = "AUTDGainCalcGetResult", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDGainCalcGetResult(GainCalcDrivesMapPtr src, DriveRaw* dst, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDGainCalcFreeResult", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDGainCalcFreeResult(GainCalcDrivesMapPtr src);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramChangeGainSegment", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDDatagramChangeGainSegment(Segment segment);

        [DllImport(__DllName, EntryPoint = "AUTDGainNull", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainNull();

        [DllImport(__DllName, EntryPoint = "AUTDGainPlane", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainPlane(double nx, double ny, double nz, byte intensity, byte phase_offset);

        [DllImport(__DllName, EntryPoint = "AUTDGainPlanelIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDGainPlanelIsDefault(GainPtr plane);

        [DllImport(__DllName, EntryPoint = "AUTDGainTransducerTest", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainTransducerTest(IntPtr f, ContextPtr context, GeometryPtr geometry);

        [DllImport(__DllName, EntryPoint = "AUTDGainUniform", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GainPtr AUTDGainUniform(byte intensity, byte phase);

        [DllImport(__DllName, EntryPoint = "AUTDGainUniformIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDGainUniformIsDefault(GainPtr uniform);

        [DllImport(__DllName, EntryPoint = "AUTDDevice", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DevicePtr AUTDDevice(GeometryPtr geo, uint dev_idx);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceNumTransducers", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDDeviceNumTransducers(DevicePtr dev);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceGetSoundSpeed", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double AUTDDeviceGetSoundSpeed(DevicePtr dev);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceSetSoundSpeed", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDDeviceSetSoundSpeed(DevicePtr dev, double value);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceSetSoundSpeedFromTemp", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDDeviceSetSoundSpeedFromTemp(DevicePtr dev, double temp, double k, double r, double m);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceGetAttenuation", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double AUTDDeviceGetAttenuation(DevicePtr dev);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceSetAttenuation", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDDeviceSetAttenuation(DevicePtr dev, double value);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceCenter", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDDeviceCenter(DevicePtr dev, double* center);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceTranslate", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDDeviceTranslate(DevicePtr dev, double x, double y, double z);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceRotate", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDDeviceRotate(DevicePtr dev, double w, double i, double j, double k);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceAffine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDDeviceAffine(DevicePtr dev, double x, double y, double z, double w, double i, double j, double k);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceEnableSet", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDDeviceEnableSet(DevicePtr dev, [MarshalAs(UnmanagedType.U1)] bool value);

        [DllImport(__DllName, EntryPoint = "AUTDDeviceEnableGet", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDDeviceEnableGet(DevicePtr dev);

        [DllImport(__DllName, EntryPoint = "AUTDGeometry", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GeometryPtr AUTDGeometry(ControllerPtr cnt);

        [DllImport(__DllName, EntryPoint = "AUTDGeometryNumDevices", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDGeometryNumDevices(GeometryPtr geo);

        [DllImport(__DllName, EntryPoint = "AUTDRotationFromEulerZYZ", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDRotationFromEulerZYZ(double x, double y, double z, double* rot);

        [DllImport(__DllName, EntryPoint = "AUTDTransducer", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern TransducerPtr AUTDTransducer(DevicePtr dev, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDTransducerPosition", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDTransducerPosition(TransducerPtr tr, double* pos);

        [DllImport(__DllName, EntryPoint = "AUTDTransducerRotation", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDTransducerRotation(TransducerPtr tr, double* rot);

        [DllImport(__DllName, EntryPoint = "AUTDTransducerDirectionX", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDTransducerDirectionX(TransducerPtr tr, double* dir);

        [DllImport(__DllName, EntryPoint = "AUTDTransducerDirectionY", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDTransducerDirectionY(TransducerPtr tr, double* dir);

        [DllImport(__DllName, EntryPoint = "AUTDTransducerDirectionZ", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDTransducerDirectionZ(TransducerPtr tr, double* dir);

        [DllImport(__DllName, EntryPoint = "AUTDTransducerWavelength", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double AUTDTransducerWavelength(TransducerPtr tr, double sound_speed);

        [DllImport(__DllName, EntryPoint = "AUTDControllerBuilder", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ControllerBuilderPtr AUTDControllerBuilder();

        [DllImport(__DllName, EntryPoint = "AUTDControllerBuilderAddDevice", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ControllerBuilderPtr AUTDControllerBuilderAddDevice(ControllerBuilderPtr builder, double x, double y, double z, double qw, double qx, double qy, double qz);

        [DllImport(__DllName, EntryPoint = "AUTDControllerOpen", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultController AUTDControllerOpen(ControllerBuilderPtr builder, LinkBuilderPtr link_builder, long timeout_ns);

        [DllImport(__DllName, EntryPoint = "AUTDControllerClose", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultI32 AUTDControllerClose(ControllerPtr cnt);

        [DllImport(__DllName, EntryPoint = "AUTDControllerDelete", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultI32 AUTDControllerDelete(ControllerPtr cnt);

        [DllImport(__DllName, EntryPoint = "AUTDControllerFPGAState", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultI32 AUTDControllerFPGAState(ControllerPtr cnt, int* @out);

        [DllImport(__DllName, EntryPoint = "AUTDControllerFirmwareInfoListPointer", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultFirmwareInfoList AUTDControllerFirmwareInfoListPointer(ControllerPtr cnt);

        [DllImport(__DllName, EntryPoint = "AUTDControllerFirmwareInfoGet", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDControllerFirmwareInfoGet(FirmwareInfoListPtr p_info_list, uint idx, byte* info);

        [DllImport(__DllName, EntryPoint = "AUTDControllerFirmwareInfoListPointerDelete", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDControllerFirmwareInfoListPointerDelete(FirmwareInfoListPtr p_info_list);

        [DllImport(__DllName, EntryPoint = "AUTDFirmwareLatest", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDFirmwareLatest(byte* latest);

        [DllImport(__DllName, EntryPoint = "AUTDControllerSend", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultI32 AUTDControllerSend(ControllerPtr cnt, DatagramPtr d1, DatagramPtr d2, long timeout_ns);

        [DllImport(__DllName, EntryPoint = "AUTDControllerGroupCreateKVMap", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern GroupKVMapPtr AUTDControllerGroupCreateKVMap();

        [DllImport(__DllName, EntryPoint = "AUTDControllerGroupKVMapSet", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultGroupKVMap AUTDControllerGroupKVMapSet(GroupKVMapPtr map, int key, DatagramPtr d1, DatagramPtr d2, long timeout_ns);

        [DllImport(__DllName, EntryPoint = "AUTDControllerGroup", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultI32 AUTDControllerGroup(ControllerPtr cnt, int* map, GroupKVMapPtr kv_map);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAudit", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkAuditBuilderPtr AUTDLinkAudit();

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditWithTimeout", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkAuditBuilderPtr AUTDLinkAuditWithTimeout(LinkAuditBuilderPtr audit, ulong timeout_ns);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditIntoBuilder", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkBuilderPtr AUTDLinkAuditIntoBuilder(LinkAuditBuilderPtr audit);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditIsOpen", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDLinkAuditIsOpen(LinkPtr audit);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditTimeoutNs", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ulong AUTDLinkAuditTimeoutNs(LinkPtr audit);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditLastTimeoutNs", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern long AUTDLinkAuditLastTimeoutNs(LinkPtr audit);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditDown", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkAuditDown(LinkPtr audit);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditBreakDown", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkAuditBreakDown(LinkPtr audit);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditCpuNumTransducers", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDLinkAuditCpuNumTransducers(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaAssertThermalSensor", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkAuditFpgaAssertThermalSensor(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaDeassertThermalSensor", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkAuditFpgaDeassertThermalSensor(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaIsForceFan", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDLinkAuditFpgaIsForceFan(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaCurrentStmSegment", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Segment AUTDLinkAuditFpgaCurrentStmSegment(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaCurrentModSegment", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern Segment AUTDLinkAuditFpgaCurrentModSegment(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaIsStmGainMode", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDLinkAuditFpgaIsStmGainMode(LinkPtr audit, Segment segment, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaSilencerUpdateRateIntensity", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ushort AUTDLinkAuditFpgaSilencerUpdateRateIntensity(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaSilencerUpdateRatePhase", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ushort AUTDLinkAuditFpgaSilencerUpdateRatePhase(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaSilencerCompletionStepsIntensity", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ushort AUTDLinkAuditFpgaSilencerCompletionStepsIntensity(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaSilencerCompletionStepsPhase", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ushort AUTDLinkAuditFpgaSilencerCompletionStepsPhase(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaSilencerFixedCompletionStepsMode", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDLinkAuditFpgaSilencerFixedCompletionStepsMode(LinkPtr audit, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaDebugTypes", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkAuditFpgaDebugTypes(LinkPtr audit, uint idx, byte* ty);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaDebugValues", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkAuditFpgaDebugValues(LinkPtr audit, uint idx, ushort* value);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaStmFrequencyDivision", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDLinkAuditFpgaStmFrequencyDivision(LinkPtr audit, Segment segment, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaStmCycle", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDLinkAuditFpgaStmCycle(LinkPtr audit, Segment segment, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaSoundSpeed", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDLinkAuditFpgaSoundSpeed(LinkPtr audit, Segment segment, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaStmLoopBehavior", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LoopBehaviorRaw AUTDLinkAuditFpgaStmLoopBehavior(LinkPtr audit, Segment segment, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaModulationFrequencyDivision", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDLinkAuditFpgaModulationFrequencyDivision(LinkPtr audit, Segment segment, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaModulationCycle", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDLinkAuditFpgaModulationCycle(LinkPtr audit, Segment segment, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaModulation", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkAuditFpgaModulation(LinkPtr audit, Segment segment, uint idx, byte* data);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaModulationLoopBehavior", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LoopBehaviorRaw AUTDLinkAuditFpgaModulationLoopBehavior(LinkPtr audit, Segment segment, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaDrives", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkAuditFpgaDrives(LinkPtr audit, Segment segment, uint idx, uint stm_idx, byte* intensities, byte* phases);

        [DllImport(__DllName, EntryPoint = "AUTDLinkAuditFpgaPhaseFilter", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkAuditFpgaPhaseFilter(LinkPtr audit, uint idx, byte* phase_filter);

        [DllImport(__DllName, EntryPoint = "AUTDLinkGet", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkPtr AUTDLinkGet(ControllerPtr cnt);

        [DllImport(__DllName, EntryPoint = "AUTDLinkNop", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkBuilderPtr AUTDLinkNop();

        [DllImport(__DllName, EntryPoint = "AUTDModulationCustom", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ModulationPtr AUTDModulationCustom(SamplingConfigurationRaw config, byte* ptr, ulong len, LoopBehaviorRaw loop_behavior);

        [DllImport(__DllName, EntryPoint = "AUTDModulationFourier", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ModulationPtr AUTDModulationFourier(ModulationPtr* components, uint size, LoopBehaviorRaw loop_behavior);

        [DllImport(__DllName, EntryPoint = "AUTDModulationSamplingConfig", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern SamplingConfigurationRaw AUTDModulationSamplingConfig(ModulationPtr m);

        [DllImport(__DllName, EntryPoint = "AUTDModulationIntoDatagramWithSegment", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDModulationIntoDatagramWithSegment(ModulationPtr m, Segment segment, [MarshalAs(UnmanagedType.U1)] bool update_segment);

        [DllImport(__DllName, EntryPoint = "AUTDModulationIntoDatagram", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDModulationIntoDatagram(ModulationPtr m);

        [DllImport(__DllName, EntryPoint = "AUTDModulationSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultI32 AUTDModulationSize(ModulationPtr m);

        [DllImport(__DllName, EntryPoint = "AUTDModulationCalc", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultModulationCalc AUTDModulationCalc(ModulationPtr m);

        [DllImport(__DllName, EntryPoint = "AUTDModulationCalcGetResult", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDModulationCalcGetResult(ModulationCalcPtr src, byte* dst);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramChangeModulationSegment", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDDatagramChangeModulationSegment(Segment segment);

        [DllImport(__DllName, EntryPoint = "AUTDModulationWithRadiationPressure", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ModulationPtr AUTDModulationWithRadiationPressure(ModulationPtr m, LoopBehaviorRaw loop_behavior);

        [DllImport(__DllName, EntryPoint = "AUTDModulationSine", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ModulationPtr AUTDModulationSine(double freq, SamplingConfigurationRaw config, byte intensity, byte offset, byte phase, SamplingMode mode, LoopBehaviorRaw loop_behavior);

        [DllImport(__DllName, EntryPoint = "AUTDModulationSineIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDModulationSineIsDefault(ModulationPtr sine);

        [DllImport(__DllName, EntryPoint = "AUTDModulationSquare", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ModulationPtr AUTDModulationSquare(double freq, SamplingConfigurationRaw config, byte low, byte high, double duty, SamplingMode mode, LoopBehaviorRaw loop_behavior);

        [DllImport(__DllName, EntryPoint = "AUTDModulationSquareIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDModulationSquareIsDefault(ModulationPtr square);

        [DllImport(__DllName, EntryPoint = "AUTDModulationStatic", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ModulationPtr AUTDModulationStatic(byte intensity, LoopBehaviorRaw loop_behavior);

        [DllImport(__DllName, EntryPoint = "AUTDModulationStaticIsDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool AUTDModulationStaticIsDefault(ModulationPtr s);

        [DllImport(__DllName, EntryPoint = "AUTDModulationWithTransform", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ModulationPtr AUTDModulationWithTransform(ModulationPtr m, IntPtr f, IntPtr context, LoopBehaviorRaw loop_behavior);

        [DllImport(__DllName, EntryPoint = "AUTDSTMFocus", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultFocusSTM AUTDSTMFocus(STMPropsPtr props, double* points, byte* intensities, ulong size);

        [DllImport(__DllName, EntryPoint = "AUTDSTMFocusIntoDatagramWithSegment", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDSTMFocusIntoDatagramWithSegment(FocusSTMPtr stm, Segment segment, [MarshalAs(UnmanagedType.U1)] bool update_segment);

        [DllImport(__DllName, EntryPoint = "AUTDSTMFocusIntoDatagram", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDSTMFocusIntoDatagram(FocusSTMPtr stm);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramChangeFocusSTMSegment", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDDatagramChangeFocusSTMSegment(Segment segment);

        [DllImport(__DllName, EntryPoint = "AUTDSTMGain", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultGainSTM AUTDSTMGain(STMPropsPtr props, GainPtr* gains, uint size, GainSTMMode mode);

        [DllImport(__DllName, EntryPoint = "AUTDSTMGainIntoDatagramWithSegment", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDSTMGainIntoDatagramWithSegment(GainSTMPtr stm, Segment segment, [MarshalAs(UnmanagedType.U1)] bool update_segment);

        [DllImport(__DllName, EntryPoint = "AUTDSTMGainIntoDatagram", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDSTMGainIntoDatagram(GainSTMPtr stm);

        [DllImport(__DllName, EntryPoint = "AUTDDatagramChangeGainSTMSegment", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern DatagramPtr AUTDDatagramChangeGainSTMSegment(Segment segment);

        [DllImport(__DllName, EntryPoint = "AUTDSTMPropsFromFreq", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern STMPropsPtr AUTDSTMPropsFromFreq(double freq);

        [DllImport(__DllName, EntryPoint = "AUTDSTMPropsFromPeriod", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern STMPropsPtr AUTDSTMPropsFromPeriod(ulong p);

        [DllImport(__DllName, EntryPoint = "AUTDSTMPropsFromSamplingConfig", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern STMPropsPtr AUTDSTMPropsFromSamplingConfig(SamplingConfigurationRaw config);

        [DllImport(__DllName, EntryPoint = "AUTDSTMPropsWithLoopBehavior", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern STMPropsPtr AUTDSTMPropsWithLoopBehavior(STMPropsPtr props, LoopBehaviorRaw loop_behavior);

        [DllImport(__DllName, EntryPoint = "AUTDSTMPropsFrequency", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern double AUTDSTMPropsFrequency(STMPropsPtr props, ulong size);

        [DllImport(__DllName, EntryPoint = "AUTDSTMPropsPeriod", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ulong AUTDSTMPropsPeriod(STMPropsPtr props, ulong size);

        [DllImport(__DllName, EntryPoint = "AUTDSTMPropsSamplingConfig", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultSamplingConfig AUTDSTMPropsSamplingConfig(STMPropsPtr props, ulong size);


    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct GroupGainMapPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct GainCalcDrivesMapPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultGainCalcDrivesMap
    {
        public GainCalcDrivesMapPtr result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ContextPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ControllerBuilderPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct FirmwareInfoListPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultFirmwareInfoList
    {
        public FirmwareInfoListPtr result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultGroupKVMap
    {
        public GroupKVMapPtr result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct GroupKVMapPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct LinkAuditBuilderPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ModulationCalcPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultModulationCalc
    {
        public ModulationCalcPtr result;
        public uint result_len;
        public uint freq_div;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct STMPropsPtr
    {
        public IntPtr Item1;
    }




}
    