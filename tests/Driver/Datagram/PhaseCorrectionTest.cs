namespace tests.Driver.Datagram;

public class PhaseCorrectionTest
{
    [Fact]
    public void PhaseCorrection()
    {
        var autd = CreateController();

        autd.Send(new AUTD3Sharp.PhaseCorrection(dev => tr => new Phase((byte)(dev.Idx() + tr.Idx()))));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x00, d));
            foreach (var tr in dev)
                Assert.Equal(dev.Idx() + tr.Idx(), phases[tr.Idx()]);
        }
    }
}
