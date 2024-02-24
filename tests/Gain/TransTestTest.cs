namespace tests.Gain;

public class TransTestTest
{
    [Fact]
    public async Task TransTest()
    {
        var autd = await AUTDTest.CreateController();

        Assert.True(await autd.SendAsync(new TransducerTest((dev, tr) =>
            (dev.Idx, tr.Idx) switch
            {
                (0, 0) => new Drive(new Phase(0x90), new EmitIntensity(0x80)),
                (1, 248) => new Drive(new Phase(0x91), new EmitIntensity(0x81)),
                _ => null
            })));

        {
            var (intensities, phases) = autd.Link.Drives(0, Segment.S0, 0);
            Assert.Equal(0x80, intensities[0]);
            Assert.Equal(0x90, phases[0]);
            Assert.All(intensities.Skip(1), d => Assert.Equal(0, d));
            Assert.All(phases.Skip(1), p => Assert.Equal(0, p));
        }

        {
            var (intensities, phases) = autd.Link.Drives(1, Segment.S0, 0);
            Assert.Equal(0x81, intensities[autd.Geometry[1].NumTransducers - 1]);
            Assert.Equal(0x91, phases[autd.Geometry[1].NumTransducers - 1]);
            Assert.All(intensities.Take(autd.Geometry[1].NumTransducers - 1), d => Assert.Equal(0, d));
            Assert.All(phases.Take(autd.Geometry[1].NumTransducers - 1), p => Assert.Equal(0, p));
        }
    }
}