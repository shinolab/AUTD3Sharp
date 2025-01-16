namespace tests.Gain;

public class UniformTest
{
    [Fact]
    public void Uniform()
    {
        var autd = AUTDTest.CreateController();

        autd.Send(new Uniform((new EmitIntensity(0x80), new Phase(0x90))));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }

        autd.Send(new Uniform((new EmitIntensity(0x81), new Phase(0x91))));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x81, d));
            Assert.All(phases, p => Assert.Equal(0x91, p));
        }
    }
}