namespace tests.Gain;

public class FocusTest
{
    [Fact]
    public async Task Focus()
    {
        var autd = await AUTDTest.CreateController();

        Assert.True(await autd.SendAsync(new Focus(autd.Geometry.Center).WithIntensity(0x80)));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }

        Assert.True(await autd.SendAsync(new Focus(autd.Geometry.Center).WithIntensity(new EmitIntensity(0x81))));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x81, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public async Task FocusDefault()
    {
#pragma warning disable CS8602, CS8605
        var autd = await AUTDTest.CreateController();
        var g = new Focus(Vector3d.zero);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDGainFocusIsDefault((AUTD3Sharp.NativeMethods.GainPtr)typeof(Focus).GetMethod("GainPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(g, new object[] { autd.Geometry })));
#pragma warning restore CS8602, CS8605
    }
}