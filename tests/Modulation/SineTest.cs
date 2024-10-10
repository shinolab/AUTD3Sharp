namespace tests.Modulation;

public class SineTest
{
    [Fact]
    public async Task SineExact()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        {
            var m = new Sine(150 * Hz).WithIntensity(0xFF / 2).WithOffset(0xFF / 2).WithPhase(MathF.PI / 2.0f * rad);
            Assert.Equal(150.0f * Hz, m.Freq);
            await autd.SendAsync(m);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                var modExpect = new byte[] {127, 125, 120, 112, 101, 88, 73, 59, 44, 30, 19, 9, 3, 0, 1, 5, 12, 22, 35, 49, 64, 78, 92,  105, 115, 122, 126,
                    127, 124, 118, 108, 97,  83, 68, 54, 39, 26, 15, 7, 2, 0, 2, 7, 15, 26, 39, 54, 68, 83, 97,  108, 118, 124, 127,
                    126, 122, 115, 105, 92,  78, 64, 49, 35, 22, 12, 5, 1, 0, 3, 9, 19, 30, 44, 59, 73, 88, 101, 112, 120, 125};
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }

        {
            var m = new Sine(150 * Hz).WithSamplingConfig(new SamplingConfig(20)).WithLoopBehavior(LoopBehavior.Once);
            Assert.Equal(LoopBehavior.Once, m.LoopBehavior);
            await autd.SendAsync(m);
            foreach (var dev in autd.Geometry)
            {
                Assert.Equal(LoopBehavior.Once, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(20u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public async Task SineExactFloat()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        {
            var m = new Sine(150.0f * Hz).WithIntensity(0xFF / 2).WithOffset(0xFF / 2).WithPhase(MathF.PI / 2.0f * rad);
            Assert.Equal(150.0f * Hz, m.Freq);
            await autd.SendAsync(m);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                var modExpect = new byte[] {127, 125, 120, 112, 101, 88, 73, 59, 44, 30, 19, 9, 3, 0, 1, 5, 12, 22, 35, 49, 64, 78, 92,  105, 115, 122, 126,
                    127, 124, 118, 108, 97,  83, 68, 54, 39, 26, 15, 7, 2, 0, 2, 7, 15, 26, 39, 54, 68, 83, 97,  108, 118, 124, 127,
                    126, 122, 115, 105, 92,  78, 64, 49, 35, 22, 12, 5, 1, 0, 3, 9, 19, 30, 44, 59, 73, 88, 101, 112, 120, 125};
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public async Task SineNearest()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var m = Sine.Nearest(150.0f * Hz);
        await autd.SendAsync(m);
        Assert.Equal(150.0f * Hz, m.Freq);
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            var modExpect = new byte[] { 128, 157, 185, 209, 230, 245, 253, 255, 250, 238, 220, 198, 171, 142,
                113, 84,  57,  35,  17,  5,   0,   2,   10,  25,  46,  70,  98 };
            Assert.Equal(modExpect, mod);
        }

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Sine(100.1f * Hz)));
        await autd.SendAsync(Sine.Nearest(100.1f * Hz));
    }

    [Fact]
    public void SineDefault()
    {
#pragma warning disable CS8602, CS8605
        var m = new Sine(0.0f * Hz);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationSineIsDefault((AUTD3Sharp.NativeMethods.ModulationPtr)typeof(Sine).GetMethod("ModulationPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(m,
            [])));
#pragma warning restore CS8602, CS8605
    }
}