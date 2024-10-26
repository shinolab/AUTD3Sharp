namespace tests.Gain;

public class FocusTest
{
    [Fact]
    public async Task Focus()
    {
        var autd = await AUTDTest.CreateController();

        var g = new Focus(autd.Geometry.Center).WithIntensity(0x81).WithPhaseOffset(0x80);
        Assert.Equal(autd.Geometry.Center, g.Pos);
        Assert.Equal(0x81, g.Intensity.Value);
        Assert.Equal(0x80, g.PhaseOffset.Value);
        await autd.SendAsync(g);
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x81, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void FocusDefault()
    {
        var g = new Focus(Vector3.Zero);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDGainFocusIsDefault(g.Intensity.Value, g.PhaseOffset.Value));
    }
}