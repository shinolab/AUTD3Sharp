namespace tests.Gain;

public class PlaneTest
{
    [Fact]
    public async Task Plane()
    {
        var autd = await AUTDTest.CreateController();

        Assert.True(await autd.SendAsync(new Plane(new Vector3d(0, 0, 1)).WithIntensity(0x80)));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }

        Assert.True(await autd.SendAsync(new Plane(new Vector3d(0, 0, 1)).WithIntensity(new EmitIntensity(0x81))));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0x81, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
    }
}
