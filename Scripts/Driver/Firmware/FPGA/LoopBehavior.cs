using System;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public readonly struct LoopBehavior
    {
        public static NativeMethods.LoopBehavior Infinite => NativeMethodsBase.AUTDLoopBehaviorInfinite();

        public static NativeMethods.LoopBehavior Finite(ushort rep)
        {
            if (rep == 0)
                throw new ArgumentOutOfRangeException(nameof(rep), "rep must be greater than 0");
            return NativeMethodsBase.AUTDLoopBehaviorFinite(rep);
        }

        public static NativeMethods.LoopBehavior Once => NativeMethodsBase.AUTDLoopBehaviorOnce();
    }
}
