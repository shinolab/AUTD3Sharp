using System;
using System.Runtime.InteropServices;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate byte ModTransformDelegate(ConstPtr context, uint i, byte d);

    [Modulation(ConfigNoChange = true, NoTransform = true)]
    public sealed partial class Transform<TM>
    where TM : IModulation
    {
        private readonly TM _m;
        private readonly ModTransformDelegate _f;

        public Transform(TM m, Func<int, byte, byte> f)
        {
            _m = m;
            _f = (_, i, d) => f((int)i, d);
        }

        private ModulationPtr ModulationPtr() => NativeMethodsBase.AUTDModulationWithTransform(_m.ModulationPtr(), new ConstPtr { Item1 = Marshal.GetFunctionPointerForDelegate(_f) }, new ConstPtr { Item1 = IntPtr.Zero }, LoopBehavior);
    }
}
