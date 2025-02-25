namespace tests.Driver.Datagram;

public class ClearTest
{
    [Fact]
    public void TestClear()
    {
        using var autd = CreateController();
        autd.Send(new Uniform(intensity: EmitIntensity.Max, phase: new Phase(0x90)));
        foreach (var dev in autd)
        {
            var m = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0xFF, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }

        autd.Send(new Clear());
        foreach (var dev in autd)
        {
            var m = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
            Assert.All(m, d => Assert.Equal(0xFF, d));
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
    }
}
