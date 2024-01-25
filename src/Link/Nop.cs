using AUTD3Sharp.Driver;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Link
{
    /// <summary>
    /// Link which do nothing
    /// </summary>
    public class Nop
    {
        public sealed class NopBuilder : ILinkBuilder<Nop>
        {
            LinkBuilderPtr ILinkBuilder<Nop>.Ptr()
            {
                return NativeMethodsBase.AUTDLinkNop();
            }

            Nop ILinkBuilder<Nop>.ResolveLink(LinkPtr ptr)
            {
                return new Nop();
            }
        }

        public static NopBuilder Builder()
        {
            return new NopBuilder();
        }
    }
}
