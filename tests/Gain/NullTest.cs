namespace tests.Gain;

public class NullTest
{
    [Fact]
    public void Null()
    {
        var autd = CreateController();

        autd.Send(new Null());

        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
    }
}
