
namespace tests.Modulation;

public class CustomTest
{
    [Fact]
    public async Task Modulation()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] { 255, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        await autd.SendAsync(new AUTD3Sharp.Modulation.Custom(modExpect, new SamplingConfig(10)));
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.Equal(modExpect, mod);
            Assert.Equal(10, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
        }
    }
}
