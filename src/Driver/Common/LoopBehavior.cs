using System;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public readonly struct LoopBehavior
    {
        public LoopBehaviorRaw Internal { get; }

        internal LoopBehavior(LoopBehaviorRaw @internal)
        {
            Internal = @internal;
        }

        public static LoopBehavior Infinite => new LoopBehavior(NativeMethodsDef.AUTDLoopBehaviorInfinite());

        public static LoopBehavior Finite(uint rep)
        {
            if (rep == 0)
                throw new ArgumentOutOfRangeException(nameof(rep), "rep must be greater than 0");
            return new LoopBehavior(NativeMethodsDef.AUTDLoopBehaviorFinite(rep));
        }

        public static LoopBehavior Once => new LoopBehavior(NativeMethodsDef.AUTDLoopBehaviorOnce());
    }
}
