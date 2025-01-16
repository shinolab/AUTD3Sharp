﻿// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AUTD3Sharp {
    public partial struct ControlPoints1
    {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public ControlPoints1 WithIntensity(global::AUTD3Sharp.EmitIntensity value)
        {
           Intensity = value;
           return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public ControlPoints1 WithIntensity(byte value)
        {
           Intensity = new EmitIntensity(value);
           return this;
        }

    }   
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
