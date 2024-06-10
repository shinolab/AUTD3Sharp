﻿// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using AUTD3Sharp;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Driver.Datagram.Modulation;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation {
    partial class Custom : AUTD3Sharp.Driver.Datagram.Modulation.IModulation, IDatagramST<ModulationPtr>, IDatagram
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        DatagramPtr IDatagram.Ptr(Geometry geometry) => NativeMethodsBase.AUTDModulationIntoDatagram(ModulationPtr(geometry));
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage] 
        DatagramPtr IDatagramST<ModulationPtr>.IntoSegmentTransition(ModulationPtr p, Segment segment, TransitionModeWrap? transitionMode) 
        => transitionMode.HasValue ? NativeMethodsBase.AUTDModulationIntoDatagramWithSegmentTransition(p, segment, transitionMode.Value) : NativeMethodsBase.AUTDModulationIntoDatagramWithSegment(p, segment);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        ModulationPtr IDatagramST<ModulationPtr>.RawPtr(Geometry geometry) => ModulationPtr(geometry);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        ModulationPtr AUTD3Sharp.Driver.Datagram.Modulation.IModulation.ModulationPtr(Geometry geometry) => ModulationPtr(geometry);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public DatagramWithSegmentTransition<Custom, ModulationPtr> WithSegment(Segment segment, TransitionModeWrap? transitionMode) => new DatagramWithSegmentTransition<Custom, ModulationPtr>(this, segment, transitionMode);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        AUTD3Sharp.NativeMethods.LoopBehavior IModulation.LoopBehavior() => LoopBehavior;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        SamplingConfigWrap IModulation.SamplingConfig() => _config;

        private SamplingConfigWrap _config = SamplingConfig.Division(5120);

        public AUTD3Sharp.NativeMethods.LoopBehavior LoopBehavior { get; private set; } = AUTD3Sharp.LoopBehavior.Infinite;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public Custom WithLoopBehavior(AUTD3Sharp.NativeMethods.LoopBehavior loopBehavior)
        {
            LoopBehavior = loopBehavior;
            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)][ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.Cache<Custom> WithCache() => new AUTD3Sharp.Driver.Datagram.Modulation.Cache<Custom>(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)][ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.RadiationPressure<Custom> WithRadiationPressure() => new AUTD3Sharp.Driver.Datagram.Modulation.RadiationPressure<Custom>(this);
        [MethodImpl(MethodImplOptions.AggressiveInlining)][ExcludeFromCodeCoverage] public AUTD3Sharp.Driver.Datagram.Modulation.Transform<Custom> WithTransform(Func<int, byte, byte> f) => new AUTD3Sharp.Driver.Datagram.Modulation.Transform<Custom>(this, f);
    }
}