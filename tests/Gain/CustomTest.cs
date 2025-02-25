namespace tests.Gain;

public class CustomTest
{
    [Fact]
    public void Custom()
    {
        var autd = CreateController();

        autd.Send(new AUTD3Sharp.Gain.Custom(dev => tr =>
            (dev.Idx(), tr.Idx()) switch
            {
                (0, 0) => new Drive() { Phase = new Phase(0x90), Intensity = new EmitIntensity(0x80) },
                (1, 248) => new Drive(new Phase(0x91), new EmitIntensity(0x81)),
                _ => Drive.Null,
            }));
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(0, Segment.S0, 0);
            Assert.Equal(0x80, intensities[0]);
            Assert.Equal(0x90, phases[0]);
            Assert.All(intensities.Skip(1), d => Assert.Equal(0, d));
            Assert.All(phases.Skip(1), p => Assert.Equal(0, p));
        }

        {
            var (intensities, phases) = autd.Link<Audit>().Drives(1, Segment.S0, 0);
            Assert.Equal(0x81, intensities[autd[1].NumTransducers() - 1]);
            Assert.Equal(0x91, phases[autd[1].NumTransducers() - 1]);
            Assert.All(intensities.Take(autd[1].NumTransducers() - 1), d => Assert.Equal(0, d));
            Assert.All(phases.Take(autd[1].NumTransducers() - 1), p => Assert.Equal(0, p));
        }
    }
}