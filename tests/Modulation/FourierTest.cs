namespace tests.Modulation;

public class FourierTest
{
    [Fact]
    public async Task FourierExact()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] { 127, 156, 183, 205, 220, 227, 226, 219, 205, 188, 170, 153, 139, 129, 124, 123, 127, 133, 140, 147, 152, 155, 154, 151, 145, 138, 131,
        125, 120, 118, 119, 122, 127, 132, 137, 140, 141, 141, 137, 133, 127, 121, 116, 113, 112, 113, 117, 121, 127, 131, 134, 135, 133, 129,
        122, 115, 108, 102, 99,  99,  101, 106, 113, 120, 127, 130, 129, 124, 115, 100, 83,  65,  48,  35,  27,  26,  34,  48,  70,  97 };

        {
            var m = new Fourier([new Sine(50 * Hz), new Sine(100 * Hz), new Sine(150 * Hz), new Sine(200 * Hz), new Sine(250 * Hz)]);
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
    public async Task FourierExactFloat()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] { 127, 156, 183, 205, 220, 227, 226, 219, 205, 188, 170, 153, 139, 129, 124, 123, 127, 133, 140, 147, 152, 155, 154, 151, 145, 138, 131,
        125, 120, 118, 119, 122, 127, 132, 137, 140, 141, 141, 137, 133, 127, 121, 116, 113, 112, 113, 117, 121, 127, 131, 134, 135, 133, 129,
        122, 115, 108, 102, 99,  99,  101, 106, 113, 120, 127, 130, 129, 124, 115, 100, 83,  65,  48,  35,  27,  26,  34,  48,  70,  97 };

        {
            var m = new Fourier([new Sine(50.0f * Hz), new Sine(100.0f * Hz), new Sine(150.0f * Hz), new Sine(200.0f * Hz), new Sine(250.0f * Hz)]);
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
    public async Task FourierNearest()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] { 127, 142, 156, 171, 184, 196, 207, 217, 225, 231, 236, 238, 239, 238, 235, 231, 225, 218, 209, 200,
                                    191, 180, 170, 160, 150, 141, 132, 124, 118, 112, 108, 105, 104, 103, 104, 106, 109, 113, 117, 122,
                                    127, 132, 136, 141, 145, 147, 149, 150, 150, 148, 146, 141, 136, 129, 121, 113, 104, 94,  83,  73,
                                    63,  53,  44,  36,  29,  23,  18,  15,  15,  15,  18,  22,  29,  36,  46,  57,  70,  83,  97,  112 };

        {
            var m = new Fourier([Sine.Nearest(50.0f * Hz), Sine.Nearest(100.0f * Hz)]);
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