namespace tests.Gain;

public class UniformTest
{
    [Fact]
    public async Task Uniform()
    {
        var autd = await AUTDTest.CreateController();

        Assert.True(await autd.SendAsync(new Uniform(0x80).WithPhase(new Phase(0x90))));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }

        Assert.True(await autd.SendAsync(new Uniform(new EmitIntensity(0x81)).WithPhase(new Phase(0x91))));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0x81, d));
            Assert.All(phases, p => Assert.Equal(0x91, p));
        }
    }

    [Fact]
    public async Task UniformDefault()
    {
#pragma warning disable CS8602, CS8605
        var autd = await AUTDTest.CreateController();
        var g = new Uniform(0);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDGainUniformIsDefault((AUTD3Sharp.NativeMethods.GainPtr)typeof(Uniform).GetMethod("GainPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(g, new object[] { autd.Geometry })));
#pragma warning restore CS8602, CS8605
    }
}