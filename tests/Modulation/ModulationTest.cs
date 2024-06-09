namespace tests.Modulation;

[Modulation]
public partial class Burst
{
    public Burst()
    {
    }

    private EmitIntensity[] Calc(Geometry geometry)
    {
        var buf = new EmitIntensity[10];
        buf[0] = EmitIntensity.Max;
        return buf;
    }
}

public class ModulationTest
{
    [Fact]
    public async Task Modulation()
    {
        var autd = await new ControllerBuilder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        await autd.SendAsync(new Burst());
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            var modExpect = new byte[] { 255, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Assert.Equal(modExpect, mod);
            Assert.Equal(5120u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
        }
    }
}
