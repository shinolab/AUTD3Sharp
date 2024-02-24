namespace tests.Gain;

public class PlaneTest
{
    [Fact]
    public async Task Plane()
    {
        var autd = await AUTDTest.CreateController();

        Assert.True(await autd.SendAsync(new Plane(new Vector3d(0, 0, 1)).WithIntensity(0x80).WithPhase(new Phase(0x81))));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x81, p));
        }

        Assert.True(await autd.SendAsync(new Plane(new Vector3d(0, 0, 1)).WithIntensity(new EmitIntensity(0x81))));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x81, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
    }

    [Fact]
    public async Task PlaneDefault()
    {
#pragma warning disable CS8602, CS8605
        var autd = await AUTDTest.CreateController();
        var g = new Plane(new Vector3d(0, 0, 0));
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDGainPlanelIsDefault((AUTD3Sharp.NativeMethods.GainPtr)typeof(Plane).GetMethod("GainPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(g, new object[] { autd.Geometry })));
#pragma warning restore CS8602, CS8605
    }
}
