using System;
using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public sealed class FociSTM : IDatagramS, IDatagram, IDatagramL
    {
        public ControlPoints[] Foci;
        public STMSamplingConfig Config;

        public FociSTM(IEnumerable<ControlPoints> foci, STMSamplingConfig config)
        {
            Foci = foci as ControlPoints[] ?? foci.ToArray();
            Config = config;
        }
        public FociSTM(IEnumerable<ControlPoint> foci, STMSamplingConfig config) : this(foci.Select(f => new ControlPoints(new[] { f })), config) { }
        public FociSTM(IEnumerable<Point3> foci, STMSamplingConfig config) : this(foci.Select(f => new ControlPoint(f)), config) { }

        public FociSTM IntoNearest() => new(Foci, Config.IntoNearest());

        public SamplingConfig SamplingConfig() => Config.SamplingConfig(Foci.Length);

        private FociSTMPtr RawPtr()
        {
            unsafe
            {
                switch (N())
                {
                    case 1:
                        var foci1 = Foci.Select(f => new ControlPoints1
                        {
                            points = f.Points[0].ToNative(),
                            intensity = f.Intensity.Inner
                        }).ToArray();
                        fixed (ControlPoints1* pFoci = &foci1[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci1.Length, N());
                    case 2:
                        var foci2 = Foci.Select(f => new ControlPoints2
                        {
                            points = (f.Points[0].ToNative(), f.Points[1].ToNative()),
                            intensity = f.Intensity.Inner
                        }).ToArray();
                        fixed (ControlPoints2* pFoci = &foci2[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci2.Length, N());
                    case 3:
                        var foci3 = Foci.Select(f => new ControlPoints3
                        {
                            points = (f.Points[0].ToNative(), f.Points[1].ToNative(), f.Points[2].ToNative()),
                            intensity = f.Intensity.Inner
                        }).ToArray();
                        fixed (ControlPoints3* pFoci = &foci3[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci3.Length, N());
                    case 4:
                        var foci4 = Foci.Select(f => new ControlPoints4
                        {
                            points = (f.Points[0].ToNative(), f.Points[1].ToNative(), f.Points[2].ToNative(), f.Points[3].ToNative()),
                            intensity = f.Intensity.Inner
                        }).ToArray();
                        fixed (ControlPoints4* pFoci = &foci4[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci4.Length, N());
                    case 5:
                        var foci5 = Foci.Select(f => new ControlPoints5
                        {
                            points = (f.Points[0].ToNative(), f.Points[1].ToNative(), f.Points[2].ToNative(), f.Points[3].ToNative(), f.Points[4].ToNative()),
                            intensity = f.Intensity.Inner
                        }).ToArray();
                        fixed (ControlPoints5* pFoci = &foci5[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci5.Length, N());
                    case 6:
                        var foci6 = Foci.Select(f => new ControlPoints6
                        {
                            points = (f.Points[0].ToNative(), f.Points[1].ToNative(), f.Points[2].ToNative(), f.Points[3].ToNative(), f.Points[4].ToNative(), f.Points[5].ToNative()),
                            intensity = f.Intensity.Inner
                        }).ToArray();
                        fixed (ControlPoints6* pFoci = &foci6[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci6.Length, N());
                    case 7:
                        var foci7 = Foci.Select(f => new ControlPoints7
                        {
                            points = (f.Points[0].ToNative(), f.Points[1].ToNative(), f.Points[2].ToNative(), f.Points[3].ToNative(), f.Points[4].ToNative(), f.Points[5].ToNative(), f.Points[6].ToNative()),
                            intensity = f.Intensity.Inner
                        }).ToArray();
                        fixed (ControlPoints7* pFoci = &foci7[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci7.Length, N());
                    default:
                        var foci8 = Foci.Select(f => new ControlPoints8
                        {
                            points = (f.Points[0].ToNative(), f.Points[1].ToNative(), f.Points[2].ToNative(), f.Points[3].ToNative(), f.Points[4].ToNative(), f.Points[5].ToNative(), f.Points[6].ToNative(), f.Points[7].ToNative()),
                            intensity = f.Intensity.Inner
                        }).ToArray();
                        fixed (ControlPoints8* pFoci = &foci8[0])
                            return NativeMethodsBase.AUTDSTMFoci(SamplingConfig().Inner, new ConstPtr { Item1 = (IntPtr)pFoci }, (ushort)foci8.Length, N());
                }
            }
        }

        DatagramPtr IDatagramS.WithSegmentTransition(Geometry _, Segment segment, TransitionMode? transitionMode) => NativeMethodsBase.AUTDSTMFociIntoDatagramWithSegment(RawPtr(), N(), segment.ToNative(), (transitionMode ?? TransitionMode.None).Inner);
        DatagramPtr IDatagramL.WithLoopBehavior(Geometry _, Segment segment, TransitionMode? transitionMode, LoopBehavior loopBehavior) => NativeMethodsBase.AUTDSTMFociIntoDatagramWithLoopBehavior(RawPtr(), N(), segment.ToNative(), (transitionMode ?? TransitionMode.None).Inner, loopBehavior.Inner);
        DatagramPtr IDatagram.Ptr(Geometry _) => NativeMethodsBase.AUTDSTMFociIntoDatagram(RawPtr(), N());

        private byte N()
        {
            if (Foci.Length == 0) throw new ArgumentOutOfRangeException();
            var n = Foci[0].Points.Length;
            if (Foci.Any(f => f.Points.Length != n)) throw new AUTDException("All components must have the same number of foci");
            return n switch
            {
                <= 8 => (byte)n,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
