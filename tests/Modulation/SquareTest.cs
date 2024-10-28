namespace tests.Modulation;

public class SquareTest
{
    [Fact]
    public async Task SquareExact()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        {
            var m = new Square(200 * Hz).WithLow(32).WithHigh(85).WithDuty(0.1f);
            Assert.Equal(200.0f * Hz, m.Freq);
            await autd.SendAsync(m);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            foreach (var dev in autd)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
#pragma warning disable IDE0230
                var modExpect = new byte[] { 85, 85, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
#pragma warning restore IDE0230
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }

        {
            var m = new Square(150 * Hz).WithSamplingConfig(new SamplingConfig(20)).WithLoopBehavior(LoopBehavior.Once);
            await autd.SendAsync(m);
            Assert.Equal(LoopBehavior.Once, m.LoopBehavior);
            foreach (var dev in autd)
            {
                Assert.Equal(LoopBehavior.Once, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(20u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public async Task SquareExactFloat()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        {
            var m = new Square(200.0f * Hz).WithLow(32).WithHigh(85).WithDuty(0.1f);
            Assert.Equal(200.0f * Hz, m.Freq);
            await autd.SendAsync(m);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            foreach (var dev in autd)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
#pragma warning disable IDE0230
                var modExpect = new byte[] { 85, 85, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
#pragma warning restore IDE0230
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public async Task SquareNearest()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var m = Square.Nearest(150.0f * Hz);
        await autd.SendAsync(m);
        Assert.Equal(150.0f * Hz, m.Freq);
        foreach (var dev in autd)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            var modExpect = new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.Equal(modExpect, mod);
        }

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Square(100.1f * Hz)));
        await autd.SendAsync(Square.Nearest(100.1f * Hz));
    }

    [Fact]
    public void SquareDefault()
    {
        var m = new Square(0.0f * Hz);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationSquareIsDefault(
            m.SamplingConfig, m.Low, m.High, m.Duty, m.LoopBehavior));
    }
}