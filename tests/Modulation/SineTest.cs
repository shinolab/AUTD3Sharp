namespace tests.Modulation;

public class SineTest
{
    [Fact]
    public void SineExact()
    {
        var autd = Controller.Builder([new AUTD3(Point3.Origin)]).Open(Audit.Builder());

        {
            var m = new Sine(150 * Hz).WithIntensity(0x80).WithOffset(0x40).WithPhase(MathF.PI / 2.0f * rad);
            Assert.Equal(150.0f * Hz, m.Freq);
            autd.Send(m);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            foreach (var dev in autd)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                var modExpect = new byte[] {128, 126, 121, 112, 101, 88, 74, 58, 44, 30, 18, 9, 3, 0, 0, 4, 12, 22, 34, 49, 64, 78, 93,  105, 115, 123, 127,
                    127, 124, 118, 109, 97,  83, 69, 53, 39, 26, 15, 6, 1, 0, 1, 6, 15, 26, 39, 53, 69, 83, 97,  109, 118, 124, 127,
                    127, 123, 115, 105, 93,  78, 64, 49, 34, 22, 12, 4, 0, 0, 3, 9, 18, 30, 44, 58, 74, 88, 101, 112, 121, 126};
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }

        {
            var m = new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(20)).WithLoopBehavior(LoopBehavior.Once);
            Assert.Equal(LoopBehavior.Once, m.LoopBehavior);
            autd.Send(m);
            foreach (var dev in autd)
            {
                Assert.Equal(LoopBehavior.Once, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(20u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public void SineExactFloat()
    {
        var autd = Controller.Builder([new AUTD3(Point3.Origin)]).Open(Audit.Builder());

        {
            var m = new Sine(150.0f * Hz).WithIntensity(0x80).WithOffset(0x40).WithPhase(MathF.PI / 2.0f * rad);
            Assert.Equal(150.0f * Hz, m.Freq);
            autd.Send(m);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            foreach (var dev in autd)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                var modExpect = new byte[] {128, 126, 121, 112, 101, 88, 74, 58, 44, 30, 18, 9, 3, 0, 0, 4, 12, 22, 34, 49, 64, 78, 93,  105, 115, 123, 127,
                    127, 124, 118, 109, 97,  83, 69, 53, 39, 26, 15, 6, 1, 0, 1, 6, 15, 26, 39, 53, 69, 83, 97,  109, 118, 124, 127,
                    127, 123, 115, 105, 93,  78, 64, 49, 34, 22, 12, 4, 0, 0, 3, 9, 18, 30, 44, 58, 74, 88, 101, 112, 121, 126};
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public void SineNearest()
    {
        var autd = Controller.Builder([new AUTD3(Point3.Origin)]).Open(Audit.Builder());

        var m = Sine.Nearest(150.0f * Hz);
        autd.Send(m);
        Assert.Equal(150.0f * Hz, m.Freq);
        foreach (var dev in autd)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            var modExpect = new byte[] { 128, 157, 185, 209, 230, 245, 253, 255, 250, 238, 220, 198, 171, 142,
                113, 84,  57,  35,  17,  5,   0,   2,   10,  25,  46,  70,  98 };
            Assert.Equal(modExpect, mod);
        }

        Assert.Throws<AUTDException>(() => autd.Send(new Sine(100.1f * Hz)));
        autd.Send(Sine.Nearest(100.1f * Hz));
    }

    [Fact]
    public void SineDefault()
    {
        var m = new Sine(0.0f * Hz);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationSineIsDefault(m.SamplingConfig, m.Intensity, m.Offset, m.Phase.Radian, m.Clamp, m.LoopBehavior));
    }
}