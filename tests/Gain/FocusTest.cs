namespace tests.Gain;

public class FocusTest
{
    [Fact]
    public async Task Focus()
    {
        var autd = await AUTDTest.CreateController();

        var g = new Focus(autd.Geometry.Center).WithIntensity(new EmitIntensity(0x81)).WithPhaseOffset(new Phase(0x80));
        Assert.Equal(autd.Geometry.Center, g.Pos);
        Assert.Equal(0x81, g.Intensity.Value);
        Assert.Equal(0x80, g.PhaseOffset.Value);
        Assert.True(await autd.SendAsync(g));
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