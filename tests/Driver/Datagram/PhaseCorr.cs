namespace tests.Driver.Datagram;

public class PhaseCorrectionTest
{
    [Fact]
    public async Task PhaseCorrection()
    {
        var autd = await AUTDTest.CreateController();

        await autd.SendAsync(new AUTD3Sharp.PhaseCorrection(dev => tr => new Phase((byte)(dev.Idx + tr.Idx))));
        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x00, d));
            foreach (var tr in dev)
                Assert.Equal(dev.Idx + tr.Idx, phases[tr.Idx]);
        }
    }
}
