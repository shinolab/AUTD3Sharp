using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Link
{
    public class Nop : Driver.Link
    {
        internal override LinkPtr Resolve() => NativeMethodsBase.AUTDLinkNop();
    }
}
