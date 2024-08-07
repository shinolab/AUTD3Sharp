using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using AUTD3Sharp.Driver;
using AUTD3Sharp.NativeMethods;
using AUTD3Sharp.Utils;

#if UNITY_2020_2_OR_NEWER
#nullable enable
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
        public float XStart { get; set; }
        public float XEnd { get; set; }
        public float YStart { get; set; }
        public float YEnd { get; set; }
        public float ZStart { get; set; }
        public float ZEnd { get; set; }
        public float Resolution { get; set; }

        internal readonly PlotRangePtr Ptr => NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotRange(XStart, XEnd, YStart, YEnd, ZStart, ZEnd, Resolution);

        public readonly Vector3[] ObservePoints()
        {
            var range = Ptr;
            var pointsLen = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotRangeObservePointsLen(range);
            var buf = new Vector3[pointsLen];
            unsafe
            {
                fixed (Vector3* p = &buf[0])
                    NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotRangeObservePoints(range, (float*)p);
            }
            return buf;
        }
    }

    public interface IPlotConfig
    {
        ConfigPtr Ptr();
        Backend Backend();
    }

    public sealed class PlotConfig : IPlotConfig
    {
        public (uint, uint) FigSize { get; set; } = (960, 640);
        public float CbarSize { get; set; } = 0.15f;
        public uint FontSize { get; set; } = 24;
        public uint LabelAreaSize { get; set; } = 80;
        public uint Margin { get; set; } = 10;
        public float TicksStep { get; set; } = 10;
        public CMap Cmap { get; set; } = CMap.Jet;
        public string Fname { get; set; } = "";

        public PlotConfigPtr RawPtr()
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(Fname);
            unsafe
            {
                fixed (byte* p = &bytes[0])
                    return NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotConfig(FigSize.Item1, FigSize.Item2, CbarSize, FontSize, LabelAreaSize, Margin, TicksStep, Cmap, p).Validate();
            }
        }

        public ConfigPtr Ptr() => new() { Item1 = RawPtr().Item1 };

        public Backend Backend() => NativeMethods.Backend.Plotters;
    }

    public sealed class PyPlotConfig : IPlotConfig
    {
        public (int, int) FigSize { get; set; } = (8, 6);
        public int Dpi { get; set; } = 72;
        public string CbarPosition { get; set; } = "right";
        public string CbarSize { get; set; } = "5%";
        public string CbarPad { get; set; } = "3%";
        public int FontSize { get; set; } = 12;
        public float TicksStep { get; set; } = 10;
        public string Cmap { get; set; } = "jet";
        public bool Show { get; set; } = false;
        public string Fname { get; set; } = "fig.png";

        public PyPlotConfigPtr RawPtr()
        {
            var cbarPosition = System.Text.Encoding.UTF8.GetBytes(CbarPosition);
            var cbarSizeBytes = System.Text.Encoding.UTF8.GetBytes(CbarSize);
            var cbarPadBytes = System.Text.Encoding.UTF8.GetBytes(CbarPad);
            var cmapBytes = System.Text.Encoding.UTF8.GetBytes(Cmap);
            var fnameBytes = System.Text.Encoding.UTF8.GetBytes(Fname);
            unsafe
            {
                fixed (byte* pCbarPosition = &cbarPosition[0])
                fixed (byte* pCbarSize = &cbarSizeBytes[0])
                fixed (byte* pCbarPad = &cbarPadBytes[0])
                fixed (byte* pCmap = &cmapBytes[0])
                fixed (byte* pFname = &fnameBytes[0])
                    return NativeMethodsLinkVisualizer.AUTDLinkVisualizerPyPlotConfig(FigSize.Item1, FigSize.Item2, Dpi, pCbarPosition, pCbarSize, pCbarPad, FontSize, TicksStep, pCmap, Show, pFname).Validate();
            }
        }

        public ConfigPtr Ptr() => new() { Item1 = RawPtr().Item1 };

        public Backend Backend() => NativeMethods.Backend.Python;
    }

    public sealed class NullPlotConfig : IPlotConfig
    {
        public ConfigPtr Ptr()
        {
            return new ConfigPtr { Item1 = NativeMethodsLinkVisualizer.AUTDLinkVisualizerNullPlotConfig().Item1 };
        }

        public Backend Backend()
        {
            return NativeMethods.Backend.Null;
        }
    }

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
                        _gpuIdx.HasValue, _gpuIdx ?? 0)
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
            Visualizer ILinkBuilder<Visualizer>.ResolveLink(RuntimePtr _, LinkPtr ptr)
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

        public Phase[] Phases(Segment segment, int idx)
        {
            unsafe
            {
                var size = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPhasesOf(_ptr, _backend, _directivity, segment,
                    (ushort)idx, null);
                var buf = new Phase[size];
                fixed (Phase* p = &buf[0])
                    _ = NativeMethodsLinkVisualizer.AUTDLinkVisualizerPhasesOf(_ptr, _backend, _directivity, segment,
                    (ushort)idx, (byte*)p);
                return buf;
            }
        }

        public EmitIntensity[] Intensities(Segment segment, int idx)
        {
            unsafe
            {
                var size = NativeMethodsLinkVisualizer.AUTDLinkVisualizerIntensities(_ptr, _backend, _directivity, segment,
                    (ushort)idx, null);
                var buf = new EmitIntensity[size];
                fixed (EmitIntensity* p = &buf[0])
                    _ = NativeMethodsLinkVisualizer.AUTDLinkVisualizerIntensities(_ptr, _backend, _directivity, segment, (ushort)idx, (byte*)p);
                return buf;
            }
        }

        public EmitIntensity[] Modulation(Segment segment)
        {
            unsafe
            {
                var size = NativeMethodsLinkVisualizer.AUTDLinkVisualizerModulation(_ptr, _backend, _directivity, segment,
                    null);
                var buf = new EmitIntensity[size];
                fixed (EmitIntensity* p = &buf[0])
                    _ = NativeMethodsLinkVisualizer.AUTDLinkVisualizerModulation(_ptr, _backend, _directivity, segment, (byte*)p);
                return buf;
            }
        }

        public System.Numerics.Complex[] CalcField(IEnumerable<Vector3> pointsIter, Segment segment, int idx)
        {
            var points = pointsIter as Vector3[] ?? pointsIter.ToArray();
            var pointsLen = points.Length;
            var buf = new float[pointsLen * 2];
            unsafe
            {
                fixed (Vector3* pp = &points[0])
                fixed (float* bp = &buf[0])
                    NativeMethodsLinkVisualizer.AUTDLinkVisualizerCalcField(_ptr, _backend, _directivity,
                        pp, (uint)pointsLen, segment, (ushort)idx, bp);
            }
            return Enumerable.Range(0, pointsLen).Select(i => new System.Numerics.Complex(buf[2 * i], buf[2 * i + 1])).ToArray();
        }

        public void PlotField(IPlotConfig config, PlotRange range, Segment segment, int idx)
        {
            if (config.Backend() != _backend) throw new AUTDException("Invalid plot config type.");
            NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotField(_ptr, _backend, _directivity, config.Ptr(), range.Ptr, segment, (ushort)idx).Validate();
        }

        public void PlotPhase(IPlotConfig config, Segment segment, int idx)
        {
            if (config.Backend() != _backend) throw new AUTDException("Invalid plot config type.");
            NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotPhase(_ptr, _backend, _directivity, config.Ptr(), segment, (ushort)idx).Validate();
        }

        public void PlotModulation(IPlotConfig config, Segment segment)
        {
            if (config.Backend() != _backend) throw new AUTDException("Invalid plot config type.");
            NativeMethodsLinkVisualizer.AUTDLinkVisualizerPlotModulation(_ptr, _backend, _directivity, config.Ptr(), segment).Validate();
        }
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
