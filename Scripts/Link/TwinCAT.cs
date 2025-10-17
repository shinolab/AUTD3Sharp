using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
using System.Runtime.CompilerServices;
#endif

namespace AUTD3Sharp.Link
{
    public sealed class TwinCAT : Driver.Link
    {
        public override LinkPtr Resolve() => NativeMethodsLinkTwinCAT.AUTDLinkTwinCAT().Validate();
    }
}
