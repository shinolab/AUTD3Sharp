using System;
using System.Net;
using System.Threading.Tasks;
using AUTD3Sharp.Driver;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp.Link
{
    public sealed class Simulator
    {
        public sealed class SimulatorBuilder : ILinkBuilder<Simulator>
        {
            private LinkSimulatorBuilderPtr _ptr;

            internal SimulatorBuilder(ushort port)
            {
                _ptr = NativeMethodsLinkSimulator.AUTDLinkSimulator(port);
            }

            public SimulatorBuilder WithServerIp(IPAddress addr)
            {
                var addrStr = addr.ToString();
                var addrBytes = System.Text.Encoding.UTF8.GetBytes(addrStr);
                unsafe
                {
                    fixed (byte* ap = &addrBytes[0])
                        _ptr = NativeMethodsLinkSimulator.AUTDLinkSimulatorWithAddr(_ptr, ap).Validate();
                }
                return this;
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

            Simulator ILinkBuilder<Simulator>.ResolveLink(LinkPtr ptr)
            {
                return new Simulator
                {
                    _ptr = ptr
                };
            }
        }

        public static SimulatorBuilder Builder(ushort port)
        {
            return new SimulatorBuilder(port);
        }

        private LinkPtr _ptr = new LinkPtr { Item1 = IntPtr.Zero };

        public async Task<bool> UpdateGeometryAsync(Geometry geometry)
        {
            return await Task.Run(() => NativeMethodsLinkSimulator.AUTDLinkSimulatorUpdateGeometry(_ptr, geometry.Ptr).Validate() == NativeMethodsDriver.AUTD3_TRUE);
        }

        public bool UpdateGeometry(Geometry geometry)
        {
            return NativeMethodsLinkSimulator.AUTDLinkSimulatorUpdateGeometry(_ptr, geometry.Ptr).Validate() == NativeMethodsDriver.AUTD3_TRUE;
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
