namespace tests.Modulation;

public class FourierTest
{
    [Fact]
    public async Task FourierExact()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] { 128, 157, 184, 206, 221, 228, 227, 219, 206, 189, 171, 154, 140, 130, 125, 124, 128, 134, 141, 148, 153, 156, 155, 152, 146, 139, 132,
            126, 121, 119, 120, 123, 128, 133, 137, 141, 142, 141, 138, 133, 128, 122, 117, 114, 113, 114, 118, 122, 128, 132, 135, 136, 134, 129,
            123, 116, 109, 103, 100, 99,  102, 107, 114, 121, 127, 131, 130, 125, 115, 101, 84,  66,  49,  36,  28,  27,  34,  49,  71,  98 };

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

        var modExpect = new byte[] { 128, 157, 184, 206, 221, 228, 227, 219, 206, 189, 171, 154, 140, 130, 125, 124, 128, 134, 141, 148, 153, 156, 155, 152, 146, 139, 132,
            126, 121, 119, 120, 123, 128, 133, 137, 141, 142, 141, 138, 133, 128, 122, 117, 114, 113, 114, 118, 122, 128, 132, 135, 136, 134, 129,
            123, 116, 109, 103, 100, 99,  102, 107, 114, 121, 127, 131, 130, 125, 115, 101, 84,  66,  49,  36,  28,  27,  34,  49,  71,  98 };

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

        var modExpect = new byte[] { 128, 142, 157, 171, 185, 197, 208, 218, 226, 232, 236, 239, 240, 239, 236, 231, 226, 218, 210, 201, 191, 181, 171, 161, 151, 141, 133,
            125, 118, 113, 109, 106, 104, 104, 105, 107, 110, 113, 118, 123, 128, 132, 137, 142, 145, 148, 150, 151, 151, 149, 146, 142, 137, 130,
            122, 114, 104, 94,  84,  74,  64,  54,  45,  37,  29,  24,  19,  16,  15,  16,  19,  23,  29,  37,  47,  58,  70,  84,  98,  113 };

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