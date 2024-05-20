namespace tests.Modulation;

public class SquareTest
{
    [Fact]
    public async Task SquareExact()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).OpenAsync(Audit.Builder());

        {
            var m = new Square(200 * Hz).WithLow(new EmitIntensity(32)).WithHigh(new EmitIntensity(85)).WithDuty(0.1);
            await autd.SendAsync(m);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
#pragma warning disable IDE0230
                var modExpect = new byte[] { 85, 85, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
#pragma warning restore IDE0230
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(5120u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }

        {
            var m = new Square(150 * Hz).WithSamplingConfig(SamplingConfig.Division(10240)).WithLoopBehavior(LoopBehavior.Once);
            await autd.SendAsync(m);
            Assert.Equal(LoopBehavior.Once, m.LoopBehavior);
            foreach (var dev in autd.Geometry)
            {
                Assert.Equal(LoopBehavior.Once, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10240u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public async Task SquareNearest()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).OpenAsync(Audit.Builder());

        await autd.SendAsync(Square.WithFreqNearest(150.0 * Hz));
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            var modExpect = new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.Equal(modExpect, mod);
        }

        await Assert.ThrowsAsync<AUTDException>(async () => await autd.SendAsync(new Square(100.1 * Hz)));
        await autd.SendAsync(Square.WithFreqNearest(100.1 * Hz));
    }

    [Fact]
    public void SquareDefault()
    {
#pragma warning disable CS8602, CS8605
        var m = new Square(0.0 * Hz);
        var autd = AUTDTest.CreateControllerSync();
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationSquareIsDefault((AUTD3Sharp.NativeMethods.ModulationPtr)typeof(Square).GetMethod("ModulationPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(m,
            [autd.Geometry])));
#pragma warning restore CS8602, CS8605
    }
}