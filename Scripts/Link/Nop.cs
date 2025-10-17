using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Link
{
    public class Nop : Driver.Link
    {
        public override LinkPtr Resolve() => NativeMethodsBase.AUTDLinkNop();
    }
}
