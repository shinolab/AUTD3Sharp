namespace tests.Gain;

public class NullTest
{
    [Fact]
    public async Task Null()
    {
        var autd = await AUTDTest.CreateController();

        Assert.True(await autd.SendAsync(new Null()));

        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
    }
}
