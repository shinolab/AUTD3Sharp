namespace tests.Driver.Datagram;

public class OutputMaskTest
{
    [Fact]
    public void OutputMask()
    {
        var autd = CreateController();

        autd.Send(new Uniform(intensity: new Intensity(0x80), phase: new Phase(0x81)));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, d => Assert.Equal(0x81, d));
        }

        autd.Send(new AUTD3Sharp.OutputMask(dev => tr => false));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x00, d));
            Assert.All(phases, d => Assert.Equal(0x81, d));
        }

        autd.Send(new WithSegment(new Uniform(intensity: new Intensity(0x80), phase: new Phase(0x81)), Segment.S1, new AUTD3Sharp.TransitionMode.Immediate()));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S1, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, d => Assert.Equal(0x81, d));
        }

        autd.Send(AUTD3Sharp.OutputMask.WithSegment(dev => tr => false, Segment.S1));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S1, 0);
            Assert.All(intensities, d => Assert.Equal(0x00, d));
            Assert.All(phases, d => Assert.Equal(0x81, d));
        }
    }
}
