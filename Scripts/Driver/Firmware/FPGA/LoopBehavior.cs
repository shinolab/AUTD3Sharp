using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct LoopBehavior
    {
        private readonly ushort _rep;

        public static LoopBehavior Infinite => NativeMethodsBase.AUTDLoopBehaviorInfinite();

        public static LoopBehavior Finite(ushort rep)
        {
            if (rep == 0)
                throw new ArgumentOutOfRangeException(nameof(rep), "rep must be greater than 0");
            return NativeMethodsBase.AUTDLoopBehaviorFinite(rep);
        }

        public static LoopBehavior Once => NativeMethodsBase.AUTDLoopBehaviorOnce();
    }
}
