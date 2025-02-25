namespace tests.Gain;

public class UniformTest
{
    [Fact]
    public void Uniform()
    {
        var autd = CreateController();

        autd.Send(new Uniform(intensity: new EmitIntensity(0x80), phase: new Phase(0x90)));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }

        autd.Send(new Uniform(intensity: new EmitIntensity(0x81), phase: new Phase(0x91)));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x81, d));
            Assert.All(phases, p => Assert.Equal(0x91, p));
        }
    }
}