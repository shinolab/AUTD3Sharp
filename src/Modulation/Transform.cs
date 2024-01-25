#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate byte ModTransformDelegate(IntPtr context, uint i, byte d);

    public class Transform<TM> : Driver.Datagram.Modulation
    where TM : Driver.Datagram.Modulation
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
            return NativeMethodsBase.AUTDModulationWithTransform(_m.ModulationPtr(), Marshal.GetFunctionPointerForDelegate(_f), IntPtr.Zero);
        }
    }

    public static class TransformModulationExtensions
    {
        public static Transform<TM> WithTransform<TM>(this TM s, Func<int, EmitIntensity, EmitIntensity> f)
        where TM : Driver.Datagram.Modulation
        {
            return new Transform<TM>(s, f);
        }
    }
}
