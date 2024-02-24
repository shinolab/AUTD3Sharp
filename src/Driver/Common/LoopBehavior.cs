#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

using System;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp
{
    public readonly struct LoopBehavior
    {
        internal LoopBehaviorRaw Internal { get; }

        internal LoopBehavior(LoopBehaviorRaw @internal)
        {
            Internal = @internal;
        }

        public static LoopBehavior Infinite => new LoopBehavior(NativeMethodsDef.AUTDLoopBehaviorInfinite());
    }
}
