﻿// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AUTD3Sharp.Modulation {
    public partial class Fourier
    {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public Fourier WithClamp(bool value)
        {
            Clamp = value;
            return this;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public Fourier WithScaleFactor(float? value)
        {
            ScaleFactor = value;
            return this;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public Fourier WithOffset(byte value)
        {
            Offset = value;
            return this;
        }
    }   
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
