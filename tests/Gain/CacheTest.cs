namespace tests.Gain;

public class CacheTest
{
    [Fact]
    public void Cache()
    {
        var autd = CreateController();
        autd.Send(new AUTD3Sharp.Gain.Cache(new Uniform(intensity: new EmitIntensity(0x80), phase: new Phase(0x90))));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }
    }
}
