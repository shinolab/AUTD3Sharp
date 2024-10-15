﻿// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AUTD3Sharp;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Gain {
    public partial class Plane : AUTD3Sharp.Driver.Datagram.Gain.IGain, IDatagramS<GainPtr>, IDatagram
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDGainIntoDatagram(((AUTD3Sharp.Driver.Datagram.Gain.IGain)this).GainPtr(geometry));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage] 
        DatagramPtr IDatagramS<GainPtr>.IntoSegment(GainPtr p, Segment segment, bool updateSegment) => NativeMethodsBase.AUTDGainIntoDatagramWithSegment(p, segment, updateSegment);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage] 
        GainPtr IDatagramS<GainPtr>.RawPtr(Geometry geometry) => GainPtr(geometry);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        GainPtr AUTD3Sharp.Driver.Datagram.Gain.IGain.GainPtr(Geometry geometry) => GainPtr(geometry);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage] 
        public DatagramWithSegment<Plane, GainPtr> WithSegment(Segment segment, bool updateSegment) => new DatagramWithSegment<Plane, GainPtr>(this, segment, updateSegment);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Gain.Cache<Plane> WithCache()
        {
            return new AUTD3Sharp.Driver.Datagram.Gain.Cache<Plane>(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Gain.Transform<Plane> WithTransform(Func<Device, Func<AUTD3Sharp.Transducer, AUTD3Sharp.Drive, AUTD3Sharp.Drive>> f)
        {
            return new AUTD3Sharp.Driver.Datagram.Gain.Transform<Plane>(this, f);
        }

    }
}