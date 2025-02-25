namespace tests.Modulation;

public class SquareTest
{
    [Fact]
    public void SquareExact()
    {
        var autd = CreateController(1);

        var m = new Square(freq: 200 * Hz, option: new SquareOption()
        {
            Low = 32,
            High = 85,
            Duty = 0.1f,
        });
        autd.Send(m);
        foreach (var dev in autd)
        {
            var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
#pragma warning disable IDE0230
            var modExpect = new byte[] { 85, 85, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
#pragma warning restore IDE0230
            Assert.Equal(modExpect, mod);
            Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(dev.Idx(), Segment.S0));
            Assert.Equal(10u, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
        }
    }

    [Fact]
    public void SquareExactFloat()
    {
        var autd = CreateController(1);

        var m = new Square(freq: 200f * Hz, option: new SquareOption()
        {
            Low = 32,
            High = 85,
            Duty = 0.1f,
        });
        autd.Send(m);
        foreach (var dev in autd)
        {
            var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
#pragma warning disable IDE0230
            var modExpect = new byte[] { 85, 85, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
#pragma warning restore IDE0230
            Assert.Equal(modExpect, mod);
            Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(dev.Idx(), Segment.S0));
            Assert.Equal(10u, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
        }
    }


    [Fact]
    public void SquareNearest()
    {
        var autd = CreateController(1);

        var m = new Square(freq: 150.0f * Hz, option: new SquareOption()).IntoNearest();
        autd.Send(m);
        foreach (var dev in autd)
        {
            var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            var modExpect = new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.Equal(modExpect, mod);
        }

        Assert.Throws<AUTDException>(() => autd.Send(new Square(freq: 100.1f * Hz, option: new SquareOption())));
        autd.Send(new Square(freq: 100.1f * Hz, option: new SquareOption()).IntoNearest());
    }

    [Fact]
    public void SquareDefault()
    {
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationSquareIsDefault(new SquareOption().ToNative()));
    }

    [Fact]
    public void InvalidType()
    {
        var m = new Square(freq: 150.0f * Hz, option: new SquareOption()).IntoNearest();
        _ = m.IntoNearest();
        Assert.Throws<AUTDException>(() => new Square(freq: 150u * Hz, option: new SquareOption()).IntoNearest());

        var autd = CreateController(1);
        m.Freq = new InvalidSamplingMode();
        Assert.Throws<AUTDException>(() => autd.Send(m));
    }

    private class InvalidSamplingMode : ISamplingMode { }
}