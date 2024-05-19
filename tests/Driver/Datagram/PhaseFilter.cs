namespace tests.Driver.Datagram;

public class PhaseFilterTest
{
    [Fact]

    public async Task PhaseFilter()
    {
        var autd = await AUTDTest.CreateController();

        Assert.True(await autd.SendAsync(new PhaseFilter((dev, tr) => new Phase((byte)(dev.Idx + tr.Idx)))));
        foreach (var dev in autd.Geometry)
        {
            var filter = autd.Link.PhaseFilter(dev.Idx);
            foreach (var tr in dev)
            {
                Assert.Equal((byte)(dev.Idx + tr.Idx), filter[tr.Idx]);
            }
        }
    }
}