namespace tests.Modulation;

using AUTD3Sharp.NativeMethods;

public class SquareTest
{
    [Fact]
    public async Task Square()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).OpenAsync(Audit.Builder());

        {
            var m = new Square(200).WithLow(new EmitIntensity(32)).WithHigh(new EmitIntensity(85)).WithDuty(0.1);
            Assert.True(await autd.SendAsync(m));
            Assert.Equal(5120u, m.SamplingConfiguration.FrequencyDivision);
            Assert.Equal(20, m.Length);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
#pragma warning disable IDE0230
                var modExpect = new byte[] { 85, 85, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
#pragma warning restore IDE0230
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(5120u, autd.Link.ModulationFrequencyDivision(dev.Idx, Segment.S0));
            }
        }

        {
            var m = new Square(150).WithSamplingConfig(SamplingConfiguration.FromFrequencyDivision(10240)).WithLoopBehavior(LoopBehavior.Once);
            Assert.True(await autd.SendAsync(m));
            Assert.Equal(10240u, m.SamplingConfiguration.FrequencyDivision);
            Assert.Equal(LoopBehavior.Once, m.LoopBehavior);
            foreach (var dev in autd.Geometry)
            {
                Assert.Equal(LoopBehavior.Once, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10240u, autd.Link.ModulationFrequencyDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public async Task SquareMode()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).OpenAsync(Audit.Builder());

        Assert.True(await autd.SendAsync(new Square(150).WithMode(SamplingMode.SizeOptimized)));
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            var modExpect = new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.Equal(modExpect, mod);
        }

        await Assert.ThrowsAsync<AUTDException>(async () => _ = await autd.SendAsync(new Square(100.1).WithMode(SamplingMode.ExactFrequency)));
        Assert.True(await autd.SendAsync(new Square(100.1).WithMode(SamplingMode.SizeOptimized)));
    }

    [Fact]
    public void SquareDefault()
    {
#pragma warning disable CS8602, CS8605
        var m = new Square(0);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDModulationSquareIsDefault((AUTD3Sharp.NativeMethods.ModulationPtr)typeof(Square).GetMethod("ModulationPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(m, new object[] { })));
#pragma warning restore CS8602, CS8605
    }
}