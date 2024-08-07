namespace tests.Modulation;

public class MixerTest
{
    [Fact]
    public async Task MixerExact()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] {7, 21, 46, 81, 115, 138, 137, 113, 75, 38, 13, 2, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 3, 4, 4, 3, 1,
                                    0, 0,  0,  0,  0,   0,   0,   0,   0,  0,  3,  6, 8, 7, 5, 2, 0, 0, 0, 0, 0, 0, 1, 2, 2, 1, 0,
                                    0, 0,  0,  0,  0,   0,   0,   0,   0,  0,  0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1};

        {
            var m = new Mixer([new Sine(50 * Hz), new Sine(100 * Hz), new Sine(150 * Hz), new Sine(200 * Hz), new Sine(250 * Hz)]);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            await autd.SendAsync(m);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public async Task MixerExactFloat()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] { 7, 21, 46, 81, 115, 138, 137, 113, 75, 38, 13, 2, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 3, 4, 4, 3, 1,
                                    0, 0,  0,  0,  0,   0,   0,   0,   0,  0,  3,  6, 8, 7, 5, 2, 0, 0, 0, 0, 0, 0, 1, 2, 2, 1, 0,
                                    0, 0,  0,  0,  0,   0,   0,   0,   0,  0,  0,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1};

        {
            var m = new Mixer([new Sine(50.0f * Hz), new Sine(100.0f * Hz), new Sine(150.0f * Hz), new Sine(200.0f * Hz), new Sine(250.0f * Hz)]);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            await autd.SendAsync(m);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public async Task MixerNearest()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] { 63,  78,  95, 113, 131, 149, 166, 183, 196, 208, 217, 222, 223, 223, 217, 208, 196, 182, 164, 146,
                                    127, 106, 87, 67,  50,  35,  22,  12,  5,   0,   0,   0,   4,   9,   17,  25,  33,  42,  50,  57,
                                    63,  67,  69, 70,  69,  66,  62,  56,  50,  43,  37,  29,  23,  17,  11,  7,   4,   2,   0,   0,
                                    0,   0,   0,  0,   1,   1,   1,   0,   0,   0,   0,   0,   1,   3,   6,   11,  17,  26,  36,  49 };

        {
            var m = new Mixer([Sine.Nearest(50.0f * Hz), Sine.Nearest(100.0f * Hz)]);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            await autd.SendAsync(m);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }
}