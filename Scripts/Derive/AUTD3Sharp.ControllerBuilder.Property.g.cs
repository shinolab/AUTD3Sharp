﻿// <auto-generated/>

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AUTD3Sharp {
    public partial class ControllerBuilder
    {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public ControllerBuilder WithDefaultParallelThreshold(ushort value)
        {
            DefaultParallelThreshold = value;
            return this;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public ControllerBuilder WithDefaultTimeout(global::AUTD3Sharp.Duration value)
        {
            DefaultTimeout = value;
            return this;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public ControllerBuilder WithSendInterval(global::AUTD3Sharp.Duration value)
        {
            SendInterval = value;
            return this;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public ControllerBuilder WithReceiveInterval(global::AUTD3Sharp.Duration value)
        {
            ReceiveInterval = value;
            return this;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public ControllerBuilder WithTimerStrategy(global::AUTD3Sharp.NativeMethods.TimerStrategyWrap value)
        {
            TimerStrategy = value;
            return this;
        }
    }   
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
