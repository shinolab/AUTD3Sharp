namespace tests.Gain;

public class BesselTest
{
    [Fact]
    public async Task Bessel()
    {
        var autd = await AUTDTest.CreateController();

        var g = new Bessel(autd.Geometry.Center, new Vector3d(0, 0, 1), Math.PI / 4).WithIntensity(new EmitIntensity(0x80)).WithPhaseOffset(new Phase(0x81));
        Assert.Equal(autd.Geometry.Center, g.Pos);
        Assert.Equal(new Vector3d(0, 0, 1), g.Dir);
        Assert.Equal(Math.PI / 4, g.Theta);
        Assert.Equal(0x80, g.Intensity.Value);
        Assert.Equal(0x81, g.PhaseOffset.Value);
        Assert.True(await autd.SendAsync(g));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public async Task BesselDefault()
    {
#pragma warning disable CS8602, CS8605
        var autd = await AUTDTest.CreateController();
        var g = new Bessel(Vector3d.zero, Vector3d.zero, 0);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDGainBesselIsDefault((AUTD3Sharp.NativeMethods.GainPtr)typeof(Bessel).GetMethod("GainPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(g, new object[] { autd.Geometry })));
#pragma warning restore CS8602, CS8605
    }
}
