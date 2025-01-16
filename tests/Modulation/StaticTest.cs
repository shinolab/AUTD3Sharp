namespace tests.Modulation;

public class StaticTest
{
    [Fact]
    public void Static()
    {
        var autd = Controller.Builder([new AUTD3(Point3.Origin)]).Open(Audit.Builder());

        {
            var m = new AUTD3Sharp.Modulation.Static();
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            autd.Send(m);
            foreach (var dev in autd)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                var modExpect = new byte[] { 0xFF, 0xFF };
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(0xFFFFu, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }

        {
            var m = AUTD3Sharp.Modulation.Static.WithIntensity(32).WithLoopBehavior(LoopBehavior.Once);
            Assert.Equal(LoopBehavior.Once, m.LoopBehavior);
            autd.Send(m);
            foreach (var dev in autd)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
#pragma warning disable IDE0230
                var modExpect = new byte[] { 32, 32 };
#pragma warning restore IDE0230
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Once, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(0xFFFFu, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }

    }

    [Fact]
    public void StaticDefault()
    {
        var m = new Static();
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationStaticIsDefault(m.Intensity));
    }
}