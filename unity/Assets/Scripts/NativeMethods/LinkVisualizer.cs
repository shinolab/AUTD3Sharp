// <auto-generated>
// This code is generated by csbindgen.
// DON'T CHANGE THIS DIRECTLY.
// </auto-generated>
#pragma warning disable CS8500
#pragma warning disable CS8981
using System;
using System.Runtime.InteropServices;


namespace AUTD3Sharp.NativeMethods
{
    public static unsafe partial class NativeMethodsLinkVisualizer
    {
        const string __DllName = "autd3capi_link_visualizer";



        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotRange", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PlotRangePtr AUTDLinkVisualizerPlotRange(float x_min, float x_max, float y_min, float y_max, float z_min, float z_max, float resolution);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotRangeObservePointsLen", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ulong AUTDLinkVisualizerPlotRangeObservePointsLen(PlotRangePtr range);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotRangeObservePoints", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void AUTDLinkVisualizerPlotRangeObservePoints(PlotRangePtr range, float* points);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPhasesOf", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDLinkVisualizerPhasesOf(LinkPtr visualizer, Backend backend, Directivity directivity, uint idx, byte* buf);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerIntensitiesOf", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDLinkVisualizerIntensitiesOf(LinkPtr visualizer, Backend backend, Directivity directivity, uint idx, byte* buf);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerModulation", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern uint AUTDLinkVisualizerModulation(LinkPtr visualizer, Backend backend, Directivity directivity, byte* buf);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerCalcFieldOf", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultI32 AUTDLinkVisualizerCalcFieldOf(LinkPtr visualizer, Backend backend, Directivity directivity, float* points, uint points_len, GeometryPtr geometry, uint idx, float* buf);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotFieldOf", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultI32 AUTDLinkVisualizerPlotFieldOf(LinkPtr visualizer, Backend backend, Directivity directivity, ConfigPtr config, PlotRangePtr range, GeometryPtr geometry, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotPhaseOf", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultI32 AUTDLinkVisualizerPlotPhaseOf(LinkPtr visualizer, Backend backend, Directivity directivity, ConfigPtr config, GeometryPtr geometry, uint idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotModulation", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultI32 AUTDLinkVisualizerPlotModulation(LinkPtr visualizer, Backend backend, Directivity directivity, ConfigPtr config);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerSphereNull", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkBuilderPtr AUTDLinkVisualizerSphereNull([MarshalAs(UnmanagedType.U1)] bool use_gpu, int gpu_idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerT4010A1Null", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkBuilderPtr AUTDLinkVisualizerT4010A1Null([MarshalAs(UnmanagedType.U1)] bool use_gpu, int gpu_idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerNullPlotConfigDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern NullPlotConfigPtr AUTDLinkVisualizerNullPlotConfigDefault();

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerSpherePlotters", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkBuilderPtr AUTDLinkVisualizerSpherePlotters([MarshalAs(UnmanagedType.U1)] bool use_gpu, int gpu_idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerT4010A1Plotters", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkBuilderPtr AUTDLinkVisualizerT4010A1Plotters([MarshalAs(UnmanagedType.U1)] bool use_gpu, int gpu_idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotConfigDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PlotConfigPtr AUTDLinkVisualizerPlotConfigDefault();

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotConfigWithFigSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PlotConfigPtr AUTDLinkVisualizerPlotConfigWithFigSize(PlotConfigPtr config, uint width, uint height);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotConfigWithCBarSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PlotConfigPtr AUTDLinkVisualizerPlotConfigWithCBarSize(PlotConfigPtr config, float cbar_size);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotConfigWithFontSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PlotConfigPtr AUTDLinkVisualizerPlotConfigWithFontSize(PlotConfigPtr config, uint font_size);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotConfigWithLabelAreaSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PlotConfigPtr AUTDLinkVisualizerPlotConfigWithLabelAreaSize(PlotConfigPtr config, uint label_area_size);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotConfigWithMargin", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PlotConfigPtr AUTDLinkVisualizerPlotConfigWithMargin(PlotConfigPtr config, uint margin);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotConfigWithTicksStep", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PlotConfigPtr AUTDLinkVisualizerPlotConfigWithTicksStep(PlotConfigPtr config, float ticks_step);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotConfigWithCMap", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PlotConfigPtr AUTDLinkVisualizerPlotConfigWithCMap(PlotConfigPtr config, CMap cmap);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPlotConfigWithFName", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultPlotConfig AUTDLinkVisualizerPlotConfigWithFName(PlotConfigPtr config, byte* fname);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerSpherePython", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkBuilderPtr AUTDLinkVisualizerSpherePython([MarshalAs(UnmanagedType.U1)] bool use_gpu, int gpu_idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerT4010A1Python", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern LinkBuilderPtr AUTDLinkVisualizerT4010A1Python([MarshalAs(UnmanagedType.U1)] bool use_gpu, int gpu_idx);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigDefault", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PyPlotConfigPtr AUTDLinkVisualizerPyPlotConfigDefault();

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigWithFigSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PyPlotConfigPtr AUTDLinkVisualizerPyPlotConfigWithFigSize(PyPlotConfigPtr config, int width, int height);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigWithDPI", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PyPlotConfigPtr AUTDLinkVisualizerPyPlotConfigWithDPI(PyPlotConfigPtr config, int dpi);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigWithCBarPosition", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultPyPlotConfig AUTDLinkVisualizerPyPlotConfigWithCBarPosition(PyPlotConfigPtr config, byte* cbar_position);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigWithCBarSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultPyPlotConfig AUTDLinkVisualizerPyPlotConfigWithCBarSize(PyPlotConfigPtr config, byte* cbar_size);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigWithCBarPad", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultPyPlotConfig AUTDLinkVisualizerPyPlotConfigWithCBarPad(PyPlotConfigPtr config, byte* cbar_pad);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigWithFontSize", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PyPlotConfigPtr AUTDLinkVisualizerPyPlotConfigWithFontSize(PyPlotConfigPtr config, int fontsize);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigWithTicksStep", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PyPlotConfigPtr AUTDLinkVisualizerPyPlotConfigWithTicksStep(PyPlotConfigPtr config, float ticks_step);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigWithCMap", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultPyPlotConfig AUTDLinkVisualizerPyPlotConfigWithCMap(PyPlotConfigPtr config, byte* cmap);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigWithShow", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern PyPlotConfigPtr AUTDLinkVisualizerPyPlotConfigWithShow(PyPlotConfigPtr config, [MarshalAs(UnmanagedType.U1)] bool show);

        [DllImport(__DllName, EntryPoint = "AUTDLinkVisualizerPyPlotConfigWithFName", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ResultPyPlotConfig AUTDLinkVisualizerPyPlotConfigWithFName(PyPlotConfigPtr config, byte* fname);


    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ConfigPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct PlotRangePtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct NullPlotConfigPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct PlotConfigPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultPlotConfig
    {
        public PlotConfigPtr result;
        public uint err_len;
        public IntPtr err;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct PyPlotConfigPtr
    {
        public IntPtr Item1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct ResultPyPlotConfig
    {
        public PyPlotConfigPtr result;
        public uint err_len;
        public IntPtr err;
    }


    public enum Backend : byte
    {
        Plotters = 0,
        Python = 1,
        Null = 2,
    }

    public enum Directivity : byte
    {
        Sphere = 0,
        T4010A1 = 1,
    }

    public enum CMap : byte
    {
        Jet = 0,
        Viridis = 1,
        Magma = 2,
        Inferno = 3,
        Plasma = 4,
        Cividis = 5,
        Turbo = 6,
        Circle = 7,
        Bluered = 8,
        Breeze = 9,
        Mist = 10,
        Earth = 11,
        Hell = 12,
    }


}
    