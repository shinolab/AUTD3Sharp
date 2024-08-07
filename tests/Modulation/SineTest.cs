namespace tests.Modulation;

public class SineTest
{
    [Fact]
    public async Task SineExact()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        {
            var m = new Sine(150 * Hz).WithIntensity(0xFF / 2).WithOffset(0xFF / 4).WithPhase(MathF.PI / 2.0f * rad);
            Assert.Equal(150.0f * Hz, m.Freq);
            await autd.SendAsync(m);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                var modExpect = new byte[] {127, 125, 120, 111, 100, 87, 73, 58, 43, 30, 18, 9, 3, 0, 0, 4, 12, 22, 34, 48, 63, 78, 92,  104, 114, 122, 126,
                                    126, 123, 117, 108, 96,  83, 68, 53, 39, 26, 15, 6, 1, 0, 1, 6, 15, 26, 39, 53, 68, 83, 96,  108, 117, 123, 126,
                                    126, 122, 114, 104, 92,  78, 63, 48, 34, 22, 12, 4, 0, 0, 3, 9, 18, 30, 43, 58, 73, 87, 100, 111, 120, 125};
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
            var m = new Sine(150.0f * Hz).WithIntensity(0xFF / 2).WithOffset(0xFF / 4).WithPhase(MathF.PI / 2.0f * rad);
            Assert.Equal(150.0f * Hz, m.Freq);
            await autd.SendAsync(m);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                var modExpect = new byte[] {127, 125, 120, 111, 100, 87, 73, 58, 43, 30, 18, 9, 3, 0, 0, 4, 12, 22, 34, 48, 63, 78, 92,  104, 114, 122, 126,
                                    126, 123, 117, 108, 96,  83, 68, 53, 39, 26, 15, 6, 1, 0, 1, 6, 15, 26, 39, 53, 68, 83, 96,  108, 117, 123, 126,
                                    126, 122, 114, 104, 92,  78, 63, 48, 34, 22, 12, 4, 0, 0, 3, 9, 18, 30, 43, 58, 73, 87, 100, 111, 120, 125};
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
            var modExpect = new byte[] { 127, 156, 184, 209, 229, 244, 253, 254, 249, 237, 220, 197, 171, 142,
                                    112, 83,  57,  34,  17,  5,   0,   1,   10,  25,  45,  70,  98 };
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