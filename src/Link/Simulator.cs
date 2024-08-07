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
            private LinkSimulatorBuilderPtr _ptr;

            internal SimulatorBuilder(IPEndPoint addr)
            {
                var addrStr = addr.ToString();
                var addrBytes = System.Text.Encoding.UTF8.GetBytes(addrStr);
                unsafe
                {
                    fixed (byte* ap = &addrBytes[0])
                        _ptr = NativeMethodsLinkSimulator.AUTDLinkSimulator(ap).Validate();
                }
            }

            public SimulatorBuilder WithTimeout(TimeSpan timeout)
            {
                _ptr = NativeMethodsLinkSimulator.AUTDLinkSimulatorWithTimeout(_ptr, (ulong)(timeout.TotalMilliseconds * 1000 * 1000));
                return this;
            }

            LinkBuilderPtr ILinkBuilder<Simulator>.Ptr()
            {
                return NativeMethodsLinkSimulator.AUTDLinkSimulatorIntoBuilder(_ptr);
            }

            Simulator ILinkBuilder<Simulator>.ResolveLink(RuntimePtr r, LinkPtr p) => new();
        }

        public static SimulatorBuilder Builder(IPEndPoint addr) => new(addr);
    }
}
