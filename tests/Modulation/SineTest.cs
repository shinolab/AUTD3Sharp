using AUTD3Sharp.Driver.Datagram;

namespace tests.Modulation;

public class SineTest
{
    [Fact]
    public void SineExact()
    {
        var autd = CreateController(1);

        var m = new Sine(freq: 150 * Hz, option: new SineOption()
        {
            Intensity = 0x80,
            Offset = 0x40,
            Phase = MathF.PI / 2.0f * rad,
        });
        Assert.Equal(SamplingConfig.Freq4K, m.SamplingConfig());
        autd.Send(m);
        foreach (var dev in autd)
        {
            var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            var modExpect = new byte[] {128, 126, 121, 112, 101, 88, 74, 58, 44, 30, 18, 9, 3, 0, 0, 4, 12, 22, 34, 49, 64, 78, 93,  105, 115, 123, 127,
                    127, 124, 118, 109, 97,  83, 69, 53, 39, 26, 15, 6, 1, 0, 1, 6, 15, 26, 39, 53, 69, 83, 97,  109, 118, 124, 127,
                    127, 123, 115, 105, 93,  78, 64, 49, 34, 22, 12, 4, 0, 0, 3, 9, 18, 30, 44, 58, 74, 88, 101, 112, 121, 126};
            Assert.Equal(modExpect, mod);
            Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(dev.Idx(), Segment.S0));
            Assert.Equal(10u, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
        }
    }

    [Fact]
    public void SineExactFloat()
    {
        var autd = CreateController(1);

        var m = new Sine(freq: 150f * Hz, option: new SineOption()
        {
            Intensity = 0x80,
            Offset = 0x40,
            Phase = MathF.PI / 2.0f * rad,
        });
        autd.Send(m);
        foreach (var dev in autd)
        {
            var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            var modExpect = new byte[] {128, 126, 121, 112, 101, 88, 74, 58, 44, 30, 18, 9, 3, 0, 0, 4, 12, 22, 34, 49, 64, 78, 93,  105, 115, 123, 127,
                    127, 124, 118, 109, 97,  83, 69, 53, 39, 26, 15, 6, 1, 0, 1, 6, 15, 26, 39, 53, 69, 83, 97,  109, 118, 124, 127,
                    127, 123, 115, 105, 93,  78, 64, 49, 34, 22, 12, 4, 0, 0, 3, 9, 18, 30, 44, 58, 74, 88, 101, 112, 121, 126};
            Assert.Equal(modExpect, mod);
            Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(dev.Idx(), Segment.S0));
            Assert.Equal(10u, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
        }

    }

    [Fact]
    public void SineNearest()
    {
        var autd = CreateController(1);

        var m = new Sine(freq: 150.0f * Hz, option: new SineOption()).IntoNearest();
        autd.Send(m);
        foreach (var dev in autd)
        {
            var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            var modExpect = new byte[] { 128, 157, 185, 209, 230, 245, 253, 255, 250, 238, 220, 198, 171, 142,
                113, 84,  57,  35,  17,  5,   0,   2,   10,  25,  46,  70,  98 };
            Assert.Equal(modExpect, mod);
        }

        Assert.Throws<AUTDException>(() => autd.Send(new Sine(100.1f * Hz, option: new SineOption())));
        autd.Send(new Sine(freq: 100.1f * Hz, option: new SineOption()).IntoNearest());
    }

    [Fact]
    public void SineDefault()
    {
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationSineIsDefault(new SineOption().ToNative()));
    }

    [Fact]
    public void InvalidType()
    {
        var m = new Sine(freq: 150.0f * Hz, option: new SineOption()).IntoNearest();
        _ = m.IntoNearest();
        Assert.Throws<AUTDException>(() => new Sine(freq: 150u * Hz, option: new SineOption()).IntoNearest());

        var autd = CreateController(1);
        m.Freq = new InvalidSamplingMode();
        Assert.Throws<AUTDException>(() => autd.Send(m));
    }

    private class InvalidSamplingMode : ISamplingMode { }
}