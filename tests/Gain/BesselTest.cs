namespace tests.Gain;

public class BesselTest
{
    [Fact]
    public async Task Bessel()
    {
        var autd = await AUTDTest.CreateController();

        Assert.True(await autd.SendAsync(new Bessel(autd.Geometry.Center, new Vector3d(0, 0, 1), Math.PI / 4).WithIntensity(0x80)));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }

        Assert.True(await autd.SendAsync(new Bessel(autd.Geometry.Center, new Vector3d(0, 0, 1), Math.PI / 4).WithIntensity(new EmitIntensity(0x81))));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x81, d));
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
