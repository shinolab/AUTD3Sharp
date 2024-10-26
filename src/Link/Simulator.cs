using System;
using System.Net;
using AUTD3Sharp.Driver;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Link
{
    public sealed class Simulator
    {
        public sealed class SimulatorBuilder : ILinkBuilder<Simulator>
        {
            public IPEndPoint Addr { get; }

            internal SimulatorBuilder(IPEndPoint addr)
            {
                Addr = addr;
            }

            LinkBuilderPtr ILinkBuilder<Simulator>.Ptr()
            {
                var addrStr = Addr.ToString();
                var addrBytes = System.Text.Encoding.UTF8.GetBytes(addrStr);
                unsafe
                {
                    fixed (byte* ap = &addrBytes[0])
                        return NativeMethodsLinkSimulator.AUTDLinkSimulator(ap).Validate();
                }
            }

            Simulator ILinkBuilder<Simulator>.ResolveLink(RuntimePtr r, LinkPtr p) => new();
        }

        public static SimulatorBuilder Builder(IPEndPoint addr) => new(addr);
    }
}
