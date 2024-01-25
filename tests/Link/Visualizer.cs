
using System.Net;
using AUTD3Sharp.NativeMethods;

namespace tests.Link;

public class VisualizerTest
{

    private void VisualizerTestWith(Controller<Visualizer> autd, IPlotConfig config)
    {
        var center = autd.Geometry.Center + new Vector3d(0, 0, 150);

        var g = new Uniform(0x80).WithPhase(new Phase(0x81));
        var m = Static.WithIntensity(0x82);

        autd.Send(m, g);

        autd.Link.PlotPhase(config, autd.Geometry);
        autd.Link.PlotField(
            config,
            new PlotRange
            {
                XStart = center[0] - 50,
                XEnd = center[0] + 50,
                YStart = center[1],
                YEnd = center[1],
                ZStart = center[2],
                ZEnd = center[2],
                Resolution = 1,
            },
            autd.Geometry
        );
        autd.Link.PlotField(
            config,
             new PlotRange
             {
                 XStart = center[0] - 20,
                 XEnd = center[0] + 20,
                 YStart = center[1] - 30,
                 YEnd = center[1] + 30,
                 ZStart = center[2],
                 ZEnd = center[2],
                 Resolution = 1,
             },
            autd.Geometry
        );
        autd.Link.PlotModulation(config);

        var intensities = autd.Link.Intensities();
        Assert.Equal(Enumerable.Range(0, autd.Geometry.NumTransducers).Select(_ => (byte)0x80).ToArray(), intensities);
        var phases = autd.Link.Phases();
        Assert.Equal(Enumerable.Range(0, autd.Geometry.NumTransducers).Select(_ => (byte)0x81).ToArray(), phases);
        var mod = autd.Link.Modulation();
        Assert.Equal(new byte[2] { 0x82, 0x82 }, mod);

        var points = new Vector3d[] { center };
        autd.Link.CalcField(points, autd.Geometry);
        autd.Link.CalcField(Enumerable.Range(0, 1).Select(_ => center), autd.Geometry);

        autd.Close();
    }


    [Fact]
    public void TestPlotRange()
    {
        var range = new PlotRange
        {
            XStart = -20,
            XEnd = 20,
            YStart = -30,
            YEnd = 30,
            ZStart = 0,
            ZEnd = 0,
            Resolution = 1,
        };
        var points = range.ObservePoints();
        Assert.Equal(41 * 61, points.Length);
        Assert.Equal(new Vector3d(-20, -30, 0), points[0]);
        Assert.Equal(new Vector3d(-19, -30, 0), points[1]);
        Assert.Equal(new Vector3d(20, 30, 0), points[^1]);
    }


    [Fact]
    public void TestPlotters()
    {
        {
            using var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWith(Visualizer.Builder().WithBackend<PlottersBackend>().WithDirectivity<Sphere>());
            VisualizerTestWith(
                   autd,
                   new PlotConfig()
               );
        }
        {
            using var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWith(Visualizer.Plotters().WithDirectivity<T4010A1>());
            VisualizerTestWith(
                   autd,
                   new PlotConfig
                   {
                       Fname = "test.png",
                       Figsize = (960, 640),
                       CbarSize = 0.15f,
                       FontSize = 24,
                       LabelAreaSize = 80,
                       Margin = 10,
                       TicksStep = 10f,
                       Cmap = CMap.Jet,
                   }
               );
        }
    }

    [Fact]
    public void TestPython()
    {
        {
            using var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWith(Visualizer.Builder().WithBackend<PythonBackend>().WithDirectivity<Sphere>());
            VisualizerTestWith(
                   autd,
                   new PyPlotConfig()
               );
        }
        {
            using var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWith(Visualizer.Python().WithDirectivity<T4010A1>());
            VisualizerTestWith(
                   autd,
                   new PyPlotConfig
                   {
                       Fname = "test.png",
                       Figsize = (6, 4),
                       Dpi = 72,
                       CbarPosition = "right",
                       CbarSize = "5%",
                       CbarPad = "3%",
                       FontSize = 12,
                       TicksStep = 10f,
                       Cmap = "jet",
                       Show = false
                   }
               );
        }
    }


    [Fact]
    public void TestNull()
    {
        {
            using var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWith(Visualizer.Builder().WithBackend<NullBackend>().WithDirectivity<Sphere>());
            VisualizerTestWith(
                    autd,
                    new NullPlotConfig()
                );
        }
        {
            using var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWith(Visualizer.Null().WithDirectivity<T4010A1>());
            VisualizerTestWith(
                   autd,
                   new NullPlotConfig()
               );
        }
    }


    [Fact]
    public void TestInvalidPlotConfig()
    {
        {
            using var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWith(Visualizer.Builder().WithBackend<PlottersBackend>().WithDirectivity<Sphere>());
            Assert.Throws<AUTDException>(() => autd.Link.PlotPhase(new NullPlotConfig(), autd.Geometry));
            Assert.Throws<AUTDException>(() => autd.Link.PlotField(
            new PyPlotConfig(),
            new PlotRange
            {
                XStart = -50,
                XEnd = 50,
                YStart = 0,
                YEnd = 0,
                ZStart = 0,
                ZEnd = 0,
                Resolution = 1,
            },
            autd.Geometry
        ));
        }

        {
            using var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWith(Visualizer.Builder().WithBackend<PythonBackend>().WithDirectivity<Sphere>());
            Assert.Throws<AUTDException>(() => autd.Link.PlotModulation(new NullPlotConfig()));
            Assert.Throws<AUTDException>(() => autd.Link.PlotPhase(new PlotConfig(), autd.Geometry));
        }

        {
            using var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenWith(Visualizer.Builder().WithBackend<NullBackend>().WithDirectivity<Sphere>());
            Assert.Throws<AUTDException>(() => autd.Link.PlotField(
            new PlotConfig(),
            new PlotRange
            {
                XStart = -50,
                XEnd = 50,
                YStart = 0,
                YEnd = 0,
                ZStart = 0,
                ZEnd = 0,
                Resolution = 1,
            },
            autd.Geometry
        ));
            Assert.Throws<AUTDException>(() => autd.Link.PlotModulation(new PyPlotConfig()));
        }
    }
}
