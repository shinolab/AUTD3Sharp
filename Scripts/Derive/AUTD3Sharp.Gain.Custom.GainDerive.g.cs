﻿// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain {
    public partial class Custom : AUTD3Sharp.Driver.Datagram.Gain.IGain, IDatagramS<GainPtr>, IDatagram
    {
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDGainIntoDatagram(((AUTD3Sharp.Driver.Datagram.Gain.IGain)this).GainPtr(geometry));
        [ExcludeFromCodeCoverage] DatagramPtr IDatagramS<GainPtr>.IntoSegment(GainPtr p, Segment segment, bool updateSegment) => NativeMethodsBase.AUTDGainIntoDatagramWithSegment(p, segment, updateSegment);
        [ExcludeFromCodeCoverage] GainPtr IDatagramS<GainPtr>.RawPtr(Geometry geometry) => GainPtr(geometry);
        GainPtr AUTD3Sharp.Driver.Datagram.Gain.IGain.GainPtr(Geometry geometry) => GainPtr(geometry);
        [ExcludeFromCodeCoverage] public DatagramWithSegment<Custom, GainPtr> WithSegment(Segment segment, bool updateSegment)
        {
            return new DatagramWithSegment<Custom, GainPtr>(this, segment, updateSegment);
        }

              
        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Gain.Cache<Custom> WithCache()
        {
            return new AUTD3Sharp.Driver.Datagram.Gain.Cache<Custom>(this);
        }

              
        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Gain.Transform<Custom> WithTransform(Func<AUTD3Sharp.Device, AUTD3Sharp.Transducer, AUTD3Sharp.Drive, AUTD3Sharp.Drive> f)
        {
            return new AUTD3Sharp.Driver.Datagram.Gain.Transform<Custom>(this, f);
        }

    }
}