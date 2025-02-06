using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

namespace AUTD3Sharp
{
    public sealed class FociSTM : IDatagramS, IDatagram, IDatagramL
    {
        public IControlPoints[] Foci;
        public STMSamplingConfig Config;

        private FociSTM(IEnumerable<IControlPoints> foci, STMSamplingConfig config)
        {
            Foci = foci as IControlPoints[] ?? foci.ToArray();
            Config = config;
        }

        public FociSTM(IEnumerable<ControlPoints1> foci, STMSamplingConfig config) : this(foci.Cast<IControlPoints>(), config) { }
        public FociSTM(IEnumerable<ControlPoint> foci, STMSamplingConfig config) : this(foci.Select(f => new ControlPoints1(f)), config) { }
        public FociSTM(IEnumerable<Point3> foci, STMSamplingConfig config) : this(foci.Select(f => new ControlPoint(f)), config) { }
        public FociSTM(IEnumerable<ControlPoints2> foci, STMSamplingConfig config) : this(foci.Cast<IControlPoints>(), config) { }
        public FociSTM(IEnumerable<ControlPoints3> foci, STMSamplingConfig config) : this(foci.Cast<IControlPoints>(), config) { }
        public FociSTM(IEnumerable<ControlPoints4> foci, STMSamplingConfig config) : this(foci.Cast<IControlPoints>(), config) { }
        public FociSTM(IEnumerable<ControlPoints5> foci, STMSamplingConfig config) : this(foci.Cast<IControlPoints>(), config) { }
        public FociSTM(IEnumerable<ControlPoints6> foci, STMSamplingConfig config) : this(foci.Cast<IControlPoints>(), config) { }
        public FociSTM(IEnumerable<ControlPoints7> foci, STMSamplingConfig config) : this(foci.Cast<IControlPoints>(), config) { }
        public FociSTM(IEnumerable<ControlPoints8> foci, STMSamplingConfig config) : this(foci.Cast<IControlPoints>(), config) { }

        public FociSTM IntoNearest() => new(Foci, Config.IntoNearest());

        public SamplingConfig SamplingConfig() => Config.SamplingConfig(Foci.Length);

        private FociSTMPtr RawPtr()
        {
            unsafe
            {
                switch (N())
                {
                    case 1:
                        var foci1 = Foci.Cast<ControlPoints1>().ToArray();
                        fixed (ControlPoints1* pFoci = &foci1[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci1.Length, N());
                    case 2:
                        var foci2 = Foci.Cast<ControlPoints2>().ToArray();
                        fixed (ControlPoints2* pFoci = &foci2[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci2.Length, N());
                    case 3:
                        var foci3 = Foci.Cast<ControlPoints3>().ToArray();
                        fixed (ControlPoints3* pFoci = &foci3[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci3.Length, N());
                    case 4:
                        var foci4 = Foci.Cast<ControlPoints4>().ToArray();
                        fixed (ControlPoints4* pFoci = &foci4[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci4.Length, N());
                    case 5:
                        var foci5 = Foci.Cast<ControlPoints5>().ToArray();
                        fixed (ControlPoints5* pFoci = &foci5[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci5.Length, N());
                    case 6:
                        var foci6 = Foci.Cast<ControlPoints6>().ToArray();
                        fixed (ControlPoints6* pFoci = &foci6[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci6.Length, N());
                    case 7:
                        var foci7 = Foci.Cast<ControlPoints7>().ToArray();
                        fixed (ControlPoints7* pFoci = &foci7[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci7.Length, N());
                    case 8:
                        var foci8 = Foci.Cast<ControlPoints8>().ToArray();
                        fixed (ControlPoints8* pFoci = &foci8[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci8.Length, N());
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        DatagramPtr IDatagramS.WithSegmentTransition(Geometry _, Segment segment, TransitionMode? transitionMode) => NativeMethodsBase.AUTDSTMFociIntoDatagramWithSegment(RawPtr(), N(), segment.ToNative(), (transitionMode ?? TransitionMode.None).Inner);
        DatagramPtr IDatagramL.WithLoopBehavior(Geometry _, Segment segment, TransitionMode? transitionMode, LoopBehavior loopBehavior) => NativeMethodsBase.AUTDSTMFociIntoDatagramWithLoopBehavior(RawPtr(), N(), segment.ToNative(), (transitionMode ?? TransitionMode.None).Inner, loopBehavior.Inner);
        DatagramPtr IDatagram.Ptr(Geometry _) => NativeMethodsBase.AUTDSTMFociIntoDatagram(RawPtr(), N());

        private byte N() => Foci[0].N;
    }
}
