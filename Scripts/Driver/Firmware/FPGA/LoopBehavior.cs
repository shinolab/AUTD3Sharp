using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#nullable enable
#endif

namespace AUTD3Sharp
{
    public class LoopBehavior : IEquatable<LoopBehavior>
    {
        [ExcludeFromCodeCoverage] private LoopBehavior() { }

        internal LoopBehavior(NativeMethods.LoopBehavior inner) => Inner = inner;

        public static LoopBehavior Infinite => new(NativeMethodsBase.AUTDLoopBehaviorInfinite());

        public static LoopBehavior Finite(ushort rep)
        {
            if (rep == 0) throw new ArgumentOutOfRangeException(nameof(rep), "rep must be greater than 0");
            return new LoopBehavior(NativeMethodsBase.AUTDLoopBehaviorFinite(rep));
        }

        public static LoopBehavior Once => new(NativeMethodsBase.AUTDLoopBehaviorFinite(1));

        internal NativeMethods.LoopBehavior Inner { get; }

        public static bool operator ==(LoopBehavior left, LoopBehavior right) => left.Equals(right);
        public static bool operator !=(LoopBehavior left, LoopBehavior right) => !left.Equals(right);
        public bool Equals(LoopBehavior? other) => other is not null && Inner.Equals(other.Inner);
        public override bool Equals(object? obj) => obj is LoopBehavior other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Inner.GetHashCode();
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
