namespace tests.Driver.Datagram.Gain;

public class CacheTest
{
    [Fact]
    public async Task Cache()
    {
        {
            var autd = await AUTDTest.CreateController();

            await autd.SendAsync(new Uniform((new EmitIntensity(0x80), new Phase(0x90))).WithCache());

            foreach (var dev in autd)
            {
                var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
                Assert.All(intensities, d => Assert.Equal(0x80, d));
                Assert.All(phases, p => Assert.Equal(0x90, p));
            }

            _ = new Uniform((new EmitIntensity(0x80), new Phase(0x90))).WithCache();
        }

        GC.Collect();
    }
}