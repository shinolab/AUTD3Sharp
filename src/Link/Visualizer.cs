/*
 * File: Visualizer.cs
 * Project: Link
 * Created Date: 13/10/2023
 * Author: Shun Suzuki
 * -----
 * Last Modified: 12/12/2023
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2023 Shun Suzuki. All rights reserved.
 *
 */



#if UNITY_2018_3_OR_NEWER
#define USE_SINGLE
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.NativeMethods;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

#if UNITY_2018_3_OR_NEWER
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
#else
using Vector3 = AUTD3Sharp.Utils.Vector3d;
using AUTD3Sharp.Internal;
#endif

#if USE_SINGLE
using float_t = System.Single;
#else
using float_t = System.Double;
#endif

namespace AUTD3Sharp.Link
{
    public interface IBackend
    {
        Backend Backend();
    }

    public sealed class PlottersBackend : IBackend
    {
        public Backend Backend() => NativeMethods.Backend.Plotters;
    }

    public sealed class PythonBackend : IBackend
    {
        public Backend Backend() => NativeMethods.Backend.Python;
    }

    public sealed class NullBackend : IBackend
    {
        public Backend Backend() => NativeMethods.Backend.Null;
    }

    public interface IDirectivity
    {
        Directivity Directivity();
    }

    public sealed class Sphere : IDirectivity
    {
        public Directivity Directivity() => NativeMethods.Directivity.Sphere;
    }

    public sealed class T4010A1 : IDirectivity
    {
        public Directivity Directivity() => NativeMethods.Directivity.T4010A1;
    }

    public struct PlotRange
    {
        public float_t XStart { get; set; }
        public float_t XEnd { get; set; }
        public float_t YStart { get; set; }
        public float_t YEnd { get; set; }
        public float_t ZStart { get; set; }
        public float_t ZEnd { get; set; }
        public float_t Resolution { get; set; }
    }

    public interface IPlotConfig
    {
        ConfigPtr Ptr();
        Backend Backend();
    }

    public sealed class PlotConfig : IPlotConfig
    {
        public (uint, uint)? Figsize { get; set; }
        public float_t? CbarSize { get; set; }
        public uint? FontSize { get; set; }
        public uint? LabelAreaSize { get; set; }
        public uint? Margin { get; set; }
        public float_t? TicksStep { get; set; }
        public CMap? Cmap { get; set; }
        public string? Fname { get; set; }

        public ConfigPtr Ptr()
        {
            var ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotConfigDefault();
            if (Figsize.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotConfigWithFigSize(ptr, Figsize.Value.Item1, Figsize.Value.Item2);
            if (CbarSize.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotConfigWithCBarSize(ptr, CbarSize.Value);
            if (FontSize.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotConfigWithFontSize(ptr, FontSize.Value);
            if (LabelAreaSize.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotConfigWithLabelAreaSize(ptr, LabelAreaSize.Value);
            if (Margin.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotConfigWithMargin(ptr, Margin.Value);
            if (TicksStep.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotConfigWithTicksStep(ptr, TicksStep.Value);
            if (Cmap.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotConfigWithCMap(ptr, Cmap.Value);
            if (Fname == null) return new ConfigPtr { Item1 = ptr.Item1 };
            var bytes = System.Text.Encoding.UTF8.GetBytes(Fname);
            unsafe
            {
                fixed (byte* p = &bytes[0])
                    ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotConfigWithFName(ptr, p).Validate();
            }
            return new ConfigPtr { Item1 = ptr.Item1 };
        }

        public Backend Backend()
        {
            return NativeMethods.Backend.Plotters;
        }
    }

    public sealed class PyPlotConfig : IPlotConfig
    {
        public (int, int)? Figsize { get; set; }
        public int? Dpi { get; set; }
        public string? CbarPosition { get; set; }
        public string? CbarSize { get; set; }
        public string? CbarPad { get; set; }
        public int? FontSize { get; set; }
        public float_t? TicksStep { get; set; }
        public string? Cmap { get; set; }
        public bool? Show { get; set; }
        public string? Fname { get; set; }

        public ConfigPtr Ptr()
        {
            var ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigDefault();
            if (Figsize.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigWithFigSize(ptr, Figsize.Value.Item1, Figsize.Value.Item2);
            if (Dpi.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigWithDPI(ptr, Dpi.Value);
            if (CbarPosition != null)
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(CbarPosition);
                unsafe
                {
                    fixed (byte* p = &bytes[0])
                        ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigWithCBarPosition(ptr, p).Validate();
                }
            }
            if (CbarSize != null)
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(CbarSize);
                unsafe
                {
                    fixed (byte* p = &bytes[0])
                        ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigWithCBarSize(ptr, p).Validate();
                }
            }
            if (CbarPad != null)
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(CbarPad);
                unsafe
                {
                    fixed (byte* p = &bytes[0])
                        ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigWithCBarPad(ptr, p).Validate();
                }
            }
            if (FontSize.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigWithFontSize(ptr, FontSize.Value);
            if (TicksStep.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigWithTicksStep(ptr, TicksStep.Value);
            if (Cmap != null)
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(Cmap);
                unsafe
                {
                    fixed (byte* p = &bytes[0])
                        ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigWithCMap(ptr, p).Validate();
                }
            }
            if (Show.HasValue)
                ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigWithShow(ptr, Show.Value);
            if (Fname == null) return new ConfigPtr { Item1 = ptr.Item1 };
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(Fname);
                unsafe
                {
                    fixed (byte* p = &bytes[0])
                        ptr = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfigWithFName(ptr, p).Validate();
                }
            }
            return new ConfigPtr { Item1 = ptr.Item1 };
        }

        public Backend Backend()
        {
            return NativeMethods.Backend.Python;
        }
    }

    public sealed class NullPlotConfig : IPlotConfig
    {
        public ConfigPtr Ptr()
        {
            return new ConfigPtr { Item1 = NativeMethodsLinkVisualizer.AUTDLinkVisualizerNullPlotConfigDefault().Item1 };
        }

        public Backend Backend()
        {
            return NativeMethods.Backend.Null;
        }
    }

    /// <summary>
    /// Link for visualizing
    /// </summary>
    public sealed class Visualizer
    {
        public sealed class VisualizerBuilder : ILinkBuilder<Visualizer>
        {
            private Backend _backend;
            private Directivity _directivity;
            private int? _gpuIdx;

            internal VisualizerBuilder(Backend backend = Backend.Plotters, Directivity directivity = Directivity.Sphere)
            {
                _backend = backend;
                _directivity = directivity;
            }

#pragma warning disable CS8524
            [ExcludeFromCodeCoverage]
            LinkBuilderPtr ILinkBuilder<Visualizer>.Ptr()
            {
                return (_backend, _directivity) switch
                {
                    (Backend.Plotters, Directivity.Sphere) => NativeMethodsLinkVisualizer.AUTDLinkVisualizerSpherePlotters(
                            _gpuIdx.HasValue, _gpuIdx ?? 0),
                    (Backend.Plotters, Directivity.T4010A1) => NativeMethodsLinkVisualizer.AUTDLinkVisualizerT4010A1Plotters(
                           _gpuIdx.HasValue, _gpuIdx ?? 0),
                    (Backend.Python, Directivity.Sphere) => NativeMethodsLinkVisualizer.AUTDLinkVisualizerSpherePython(
                            _gpuIdx.HasValue, _gpuIdx ?? 0),
                    (Backend.Python, Directivity.T4010A1) => NativeMethodsLinkVisualizer.AUTDLinkVisualizerT4010A1Python(
                            _gpuIdx.HasValue, _gpuIdx ?? 0),
                    (Backend.Null, Directivity.Sphere) => NativeMethodsLinkVisualizer.AUTDLinkVisualizerSphereNull(
                             _gpuIdx.HasValue, _gpuIdx ?? 0),
                    (Backend.Null, Directivity.T4010A1) => NativeMethodsLinkVisualizer.AUTDLinkVisualizerT4010A1Null(
                        _gpuIdx.HasValue, _gpuIdx ?? 0),
                };
            }
#pragma warning restore CS8524

            [ExcludeFromCodeCoverage]
            public VisualizerBuilder WithGpu(int gpuIdx)
            {
                _gpuIdx = gpuIdx;
                return this;
            }
            public VisualizerBuilder WithBackend<TB>()
            where TB : IBackend, new()
            {
                _backend = new TB().Backend();
                return this;
            }

            public VisualizerBuilder WithDirectivity<TD>()
                where TD : IDirectivity, new()
            {
                _directivity = new TD().Directivity();
                return this;
            }
            Visualizer ILinkBuilder<Visualizer>.ResolveLink(LinkPtr ptr)
            {
                return new Visualizer
                {
                    _ptr = ptr,
                    _backend = _backend,
                    _directivity = _directivity
                };
            }
        }

        public static VisualizerBuilder Builder()
        {
            return new VisualizerBuilder();
        }

        public static VisualizerBuilder Plotters()
        {
            return new VisualizerBuilder();
        }

        public static VisualizerBuilder Python()
        {
            return new VisualizerBuilder(Backend.Python);
        }

        public static VisualizerBuilder Null()
        {
            return new VisualizerBuilder(Backend.Null);
        }

        private LinkPtr _ptr;
        private Backend _backend;
        private Directivity _directivity;

        public byte[] PhasesOf(int idx)
        {
            unsafe
            {
                var size = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPhasesOf(_ptr, _backend, _directivity,
                    (uint)(idx), null);
                var buf = new byte[size];
                fixed (byte* p = &buf[0])
                    NativeMethodsLinkVisualizer.AUTDLinkVisualizerPhasesOf(_ptr, _backend, _directivity,
                    (uint)(idx), p);
                return buf;
            }
        }

        public byte[] Phases()
        {
            return PhasesOf(0);
        }

        public byte[] IntensitiesOf(int idx)
        {
            unsafe
            {
                var size = NativeMethodsLinkVisualizer.AUTDLinkVisualizerIntensitiesOf(_ptr, _backend, _directivity,
                    (uint)(idx), null);
                var buf = new byte[size];
                fixed (byte* p = &buf[0])
                    NativeMethodsLinkVisualizer.AUTDLinkVisualizerIntensitiesOf(_ptr, _backend, _directivity,
                    (uint)(idx), p);
                return buf;
            }
        }

        public byte[] Intensities()
        {
            return IntensitiesOf(0);
        }

        public byte[] Modulation()
        {
            unsafe
            {
                var size = NativeMethodsLinkVisualizer.AUTDLinkVisualizerModulation(_ptr, _backend, _directivity,
                    null);
                var buf = new byte[size];
                fixed (byte* p = &buf[0])
                    NativeMethodsLinkVisualizer.AUTDLinkVisualizerModulation(_ptr, _backend, _directivity, p);
                return buf;
            }
        }

        public System.Numerics.Complex[] CalcFieldOf(IEnumerable<Vector3> pointsIter, Geometry geometry, int idx)
        {
            var points = pointsIter as Vector3[] ?? pointsIter.ToArray();
            var pointsLen = points.Count();
            var pointsPtr = points.SelectMany(v => new[] { v.x, v.y, v.z }).ToArray();
            var buf = new float_t[pointsLen * 2];
            unsafe
            {
                fixed (float_t* pp = &pointsPtr[0])
                fixed (float_t* bp = &buf[0])
                    NativeMethodsLinkVisualizer.AUTDLinkVisualizerCalcFieldOf(_ptr, _backend, _directivity,
                        pp, (uint)pointsLen, geometry.Ptr, (uint)(idx), bp);
            }
            return Enumerable.Range(0, pointsLen).Select(i => new System.Numerics.Complex(buf[2 * i], buf[2 * i + 1])).ToArray();
        }

        public System.Numerics.Complex[] CalcField(IEnumerable<Vector3> pointsIter, Geometry geometry)
        {
            return CalcFieldOf(pointsIter, geometry, 0);
        }

        public void PlotFieldOf(IPlotConfig config, PlotRange range, Geometry geometry, int idx)
        {
            if (config.Backend() != _backend) throw new AUTDException("Invalid plot config type.");
            NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotFieldOf(_ptr, _backend, _directivity, config.Ptr(), NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotRange(range.XStart, range.XEnd, range.YStart, range.YEnd, range.ZStart, range.ZEnd, range.Resolution), geometry.Ptr, (uint)idx).Validate();
        }

        public void PlotField(IPlotConfig config, PlotRange range, Geometry geometry)
        {
            PlotFieldOf(config, range, geometry, 0);
        }

        public void PlotPhaseOf(IPlotConfig config, Geometry geometry, int idx)
        {
            if (config.Backend() != _backend) throw new AUTDException("Invalid plot config type.");
            NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotPhaseOf(_ptr, _backend, _directivity, config.Ptr(), geometry.Ptr, (uint)idx).Validate();
        }

        public void PlotPhase(IPlotConfig config, Geometry geometry)
        {
            PlotPhaseOf(config, geometry, 0);
        }

        public void PlotModulation(IPlotConfig config)
        {
            if (config.Backend() != _backend) throw new AUTDException("Invalid plot config type.");
            NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotModulation(_ptr, _backend, _directivity, config.Ptr()).Validate();
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
