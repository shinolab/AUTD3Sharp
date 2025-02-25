namespace tests.Modulation;

public class FourierTest
{
    [Fact]
    public void FourierExact()
    {
        var autd = CreateController(1);

        var modExpect = new byte[] { 128, 157, 184, 206, 221, 228, 227, 219, 206, 189, 171, 154, 140, 130, 125, 124, 128, 134, 141, 148, 153, 156, 155, 152, 146, 139, 132,
            126, 121, 119, 120, 123, 128, 133, 137, 141, 142, 141, 138, 133, 128, 122, 117, 114, 113, 114, 118, 122, 128, 132, 135, 136, 134, 129,
            123, 116, 109, 103, 100, 99,  102, 107, 114, 121, 127, 131, 130, 125, 115, 101, 84,  66,  49,  36,  28,  27,  34,  49,  71,  98 };

        var m = new Fourier([new Sine(freq: 50 * Hz, option: new SineOption()), new Sine(freq: 100 * Hz, option: new SineOption()), new Sine(freq: 150 * Hz, option: new SineOption()), new Sine(freq: 200 * Hz, option: new SineOption()), new Sine(freq: 250 * Hz, option: new SineOption())], option: new FourierOption());
        autd.Send(m);
        foreach (var dev in autd)
        {
            var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.Equal(modExpect, mod);
            Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(dev.Idx(), Segment.S0));
            Assert.Equal(10u, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
        }
    }

    [Fact]
    public void FourierExactFloat()
    {
        var autd = CreateController(1);

        var modExpect = new byte[] { 128, 157, 184, 206, 221, 228, 227, 219, 206, 189, 171, 154, 140, 130, 125, 124, 128, 134, 141, 148, 153, 156, 155, 152, 146, 139, 132,
            126, 121, 119, 120, 123, 128, 133, 137, 141, 142, 141, 138, 133, 128, 122, 117, 114, 113, 114, 118, 122, 128, 132, 135, 136, 134, 129,
            123, 116, 109, 103, 100, 99,  102, 107, 114, 121, 127, 131, 130, 125, 115, 101, 84,  66,  49,  36,  28,  27,  34,  49,  71,  98 };

        var m = new Fourier([new Sine(freq: 50.0f * Hz, option: new SineOption()), new Sine(freq: 100.0f * Hz, option: new SineOption()), new Sine(freq: 150.0f * Hz, option: new SineOption()), new Sine(freq: 200.0f * Hz, option: new SineOption()), new Sine(freq: 250.0f * Hz, option: new SineOption())], option: new FourierOption());
        autd.Send(m);
        foreach (var dev in autd)
        {
            var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.Equal(modExpect, mod);
            Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(dev.Idx(), Segment.S0));
            Assert.Equal(10u, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
        }
    }

    [Fact]
    public void FourierNearest()
    {
        var autd = CreateController(1);

        var modExpect = new byte[] { 128, 142, 157, 171, 185, 197, 208, 218, 226, 232, 236, 239, 240, 239, 236, 231, 226, 218, 210, 201, 191, 181, 171, 161, 151, 141, 133,
            125, 118, 113, 109, 106, 104, 104, 105, 107, 110, 113, 118, 123, 128, 132, 137, 142, 145, 148, 150, 151, 151, 149, 146, 142, 137, 130,
            122, 114, 104, 94,  84,  74,  64,  54,  45,  37,  29,  24,  19,  16,  15,  16,  19,  23,  29,  37,  47,  58,  70,  84,  98,  113 };

        var m = new Fourier([new Sine(freq: 50.0f * Hz, option: new SineOption()).IntoNearest(), new Sine(freq: 100.0f * Hz, option: new SineOption()).IntoNearest()], option: new FourierOption());
        autd.Send(m);
        foreach (var dev in autd)
        {
            var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.Equal(modExpect, mod);
            Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(dev.Idx(), Segment.S0));
            Assert.Equal(10u, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
        }
    }

    [Fact]
    public void InvalidType()
    {
        var autd = CreateController(1);
        Assert.Throws<AUTDException>(() => autd.Send(new Fourier([new Sine(freq: 50u * Hz, option: new SineOption()), new Sine(freq: 50f * Hz, option: new SineOption())], option: new FourierOption())));

        var s = new Sine(freq: 50 * Hz, option: new SineOption())
        {
            Freq = new InvalidSamplingMode()
        };
        Assert.Throws<AUTDException>(() => autd.Send(new Fourier([s], option: new FourierOption())));
    }

    private class InvalidSamplingMode : ISamplingMode { }
}
