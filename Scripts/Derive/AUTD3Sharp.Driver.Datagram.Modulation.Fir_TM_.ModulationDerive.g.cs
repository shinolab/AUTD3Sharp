﻿// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AUTD3Sharp;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Datagram.Modulation;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation {
    partial class Fir<TM> : AUTD3Sharp.Driver.Datagram.Modulation.IModulation, IDatagramS<ModulationPtr>, IDatagram
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDModulationIntoDatagram(ModulationPtr());
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage] 
        DatagramPtr IDatagramS<ModulationPtr>.IntoSegmentTransition(ModulationPtr p, Segment segment, TransitionModeWrap? transitionMode) 
        => NativeMethodsBase.AUTDModulationIntoDatagramWithSegment(p, segment, transitionMode ?? TransitionMode.None);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        ModulationPtr IDatagramS<ModulationPtr>.RawPtr(Geometry geometry) => ModulationPtr();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        ModulationPtr AUTD3Sharp.Driver.Datagram.Modulation.IModulation.ModulationPtr() => ModulationPtr();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public DatagramWithSegment<Fir<TM>, ModulationPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode) => new DatagramWithSegment<Fir<TM>, ModulationPtr>(this, segment, transitionMode);
        
        [ExcludeFromCodeCoverage]
        public AUTD3Sharp.SamplingConfig SamplingConfig => new(NativeMethodsBase.AUTDModulationSamplingConfig(ModulationPtr()));

        private AUTD3Sharp.SamplingConfig _config = new(10);

        public AUTD3Sharp.LoopBehavior LoopBehavior => _loopBehavior;

        private AUTD3Sharp.LoopBehavior _loopBehavior = AUTD3Sharp.LoopBehavior.Infinite;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public Fir<TM> WithLoopBehavior(AUTD3Sharp.LoopBehavior loopBehavior)
        {
            _loopBehavior = loopBehavior;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)][ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.Cache<Fir<TM>> WithCache() => new AUTD3Sharp.Driver.Datagram.Modulation.Cache<Fir<TM>>(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)][ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.RadiationPressure<Fir<TM>> WithRadiationPressure() => new AUTD3Sharp.Driver.Datagram.Modulation.RadiationPressure<Fir<TM>>(this);
        [MethodImpl(MethodImplOptions.AggressiveInlining)][ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.Fir<Fir<TM>> WithFir(IEnumerable<float> iter) => new AUTD3Sharp.Driver.Datagram.Modulation.Fir<Fir<TM>>(this, iter);
    }
}