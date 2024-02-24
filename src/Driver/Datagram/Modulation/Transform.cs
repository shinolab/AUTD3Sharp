#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate byte ModTransformDelegate(IntPtr context, uint i, byte d);

    [Modulation(ConfigNoChange = true, NoTransform = true)]
    public sealed partial class Transform<TM> : Modulation
    where TM : Modulation
    {
        private readonly TM _m;
        private readonly ModTransformDelegate _f;

        public Transform(TM m, Func<int, EmitIntensity, EmitIntensity> f)
        {
            _m = m;
            _f = (context, i, d) => f((int)i, new EmitIntensity(d)).Value;
        }

        internal override ModulationPtr ModulationPtr()
        {
            return NativeMethodsBase.AUTDModulationWithTransform(_m.ModulationPtr(), Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero, LoopBehavior.Internal);
        }
    }
}
