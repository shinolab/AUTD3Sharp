﻿// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;

namespace AUTD3Sharp.Gain {
    public partial class Plane
    {
        
        public Plane WithPhaseOffset(global::AUTD3Sharp.Phase value)
        {
            PhaseOffset = value;
            return this;
        }
        public Plane WithIntensity(global::AUTD3Sharp.EmitIntensity value)
        {
           Intensity = value;
           return this;
        }

        [ExcludeFromCodeCoverage] public Plane WithIntensity(byte value)
        {
           Intensity = new EmitIntensity(value);
           return this;
        }
    }   
}