using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public static class TransitionMode
    {
        public static TransitionModeWrap SyncIdx = NativeMethodsBase.AUTDTransitionModeSyncIdx();
        public static TransitionModeWrap SysTime(DcSysTime sysTime) => NativeMethodsBase.AUTDTransitionModeSysTime(sysTime.SysTime);
        public static TransitionModeWrap GPIO(GPIOIn gpio) => NativeMethodsBase.AUTDTransitionModeGPIO(gpio);
        public static TransitionModeWrap Ext = NativeMethodsBase.AUTDTransitionModeExt();
        public static TransitionModeWrap Immediate = NativeMethodsBase.AUTDTransitionModeImmediate();
    }
}
