namespace tests.Modulation;

using AUTD3Sharp.NativeMethods;

public class SquareTest
{
    [Fact]
    public async Task Square()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).OpenWithAsync(Audit.Builder());

        Assert.True(await autd.SendAsync(new Square(200).WithLow(new EmitIntensity(32)).WithHigh(new EmitIntensity(85)).WithDuty(0.1)));
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx);
#pragma warning disable IDE0230
            var modExpect = new byte[] { 85, 85, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
#pragma warning restore IDE0230
            Assert.Equal(modExpect, mod);
            Assert.Equal(5120u, autd.Link.ModulationFrequencyDivision(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new Square(200).WithLow(32).WithHigh(85).WithDuty(0.1)));
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx);
#pragma warning disable IDE0230
            var modExpect = new byte[] { 85, 85, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32 };
#pragma warning restore IDE0230
            Assert.Equal(modExpect, mod);
            Assert.Equal(5120u, autd.Link.ModulationFrequencyDivision(dev.Idx));
        }

        Assert.True(await autd.SendAsync(new Square(150).WithSamplingConfig(SamplingConfiguration.FromFrequencyDivision(10240))));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(10240u, autd.Link.ModulationFrequencyDivision(dev.Idx));
        }
    }

    [Fact]
    public async Task SquareMode()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).OpenWithAsync(Audit.Builder());

        Assert.True(await autd.SendAsync(new Square(150).WithMode(SamplingMode.SizeOptimized)));
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx);
            var modExpect = new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.Equal(modExpect, mod);
        }

        await Assert.ThrowsAsync<AUTDException>(async () => _ = await autd.SendAsync(new Square(100.1).WithMode(SamplingMode.ExactFrequency)));
        Assert.True(await autd.SendAsync(new Square(100.1).WithMode(SamplingMode.SizeOptimized)));
    }
}