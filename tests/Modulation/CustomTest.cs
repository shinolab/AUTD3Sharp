
namespace tests.Modulation;

public class CustomTest
{
    [Fact]
    public async Task ModulationCustom()
    {
        var autd = await Controller.Builder([new AUTD3(Point3.Origin)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] { 255, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        await autd.SendAsync(new AUTD3Sharp.Modulation.Custom(modExpect, new SamplingConfig(10)));
        foreach (var dev in autd)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.Equal(modExpect, mod);
            Assert.Equal(10, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
        }
    }

    [Fact]
    public async Task ModulationCustomResample()
    {
        var autd = await Controller.Builder([new AUTD3(Point3.Origin)]).OpenAsync(Audit.Builder());

        await autd.SendAsync(new AUTD3Sharp.Modulation.Custom([127, 255, 127, 0], 2.0f * kHz, 4 * kHz, new SincInterpolation()));
        foreach (var dev in autd)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.Equal(new byte[] { 127, 217, 255, 217, 127, 37, 0, 37 }, mod);
            Assert.Equal(10, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
        }

        await autd.SendAsync(new AUTD3Sharp.Modulation.Custom([127, 255, 127, 0], 2.0f * kHz, 4 * kHz, new SincInterpolation(new Rectangular(32))));
        foreach (var dev in autd)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            Assert.Equal(new byte[] { 127, 217, 255, 223, 127, 42, 0, 37 }, mod);
            Assert.Equal(10, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
        }
    }
}
