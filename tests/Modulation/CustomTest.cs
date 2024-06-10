
namespace tests.Modulation;

public class CustomTest
{
    [Fact]
    public async Task Modulation()
    {
        var autd = await new ControllerBuilder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] { 255, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        await autd.SendAsync(new AUTD3Sharp.Modulation.Custom(modExpect, SamplingConfig.Division(5120)));
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.Equal(modExpect, mod);
            Assert.Equal(5120u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
        }
    }
}
