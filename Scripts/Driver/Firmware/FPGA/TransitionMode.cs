using AUTD3Sharp.NativeMethods;
using System;
using System.Diagnostics.CodeAnalysis;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public class TransitionMode : IEquatable<TransitionMode>
    {
        internal readonly TransitionModeWrap Inner;

        private TransitionMode(TransitionModeWrap inner) { Inner = inner; }

        public static TransitionMode SyncIdx => new(NativeMethodsBase.AUTDTransitionModeSyncIdx());
        public static TransitionMode SysTime(DcSysTime sysTime) => new(NativeMethodsBase.AUTDTransitionModeSysTime(sysTime));
        public static TransitionMode GPIO(GPIOIn gpio) => new(NativeMethodsBase.AUTDTransitionModeGPIO(gpio.ToNative()));
        public static readonly TransitionMode Ext = new(NativeMethodsBase.AUTDTransitionModeExt());
        public static readonly TransitionMode Immediate = new(NativeMethodsBase.AUTDTransitionModeImmediate());

        internal static TransitionMode None = new(NativeMethodsBase.AUTDTransitionModeNone());

        public static bool operator ==(TransitionMode left, TransitionMode right) => left.Equals(right);
        public static bool operator !=(TransitionMode left, TransitionMode right) => !left.Equals(right);
        public bool Equals(TransitionMode? other) => other is not null && Inner.Equals(other.Inner);
        public override bool Equals(object? obj) => obj is TransitionMode other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Inner.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
