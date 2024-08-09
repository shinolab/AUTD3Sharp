﻿// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AUTD3Sharp.Modulation {
    public partial class Sine
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public Sine WithIntensity(byte value)
        {
            Intensity = value;
            return this;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public Sine WithOffset(byte value)
        {
            Offset = value;
            return this;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public Sine WithPhase(global::AUTD3Sharp.Angle value)
        {
            Phase = value;
            return this;
        }
    }   
}