namespace tests.Modulation;

public class FourierTest
{
    [Fact]
    public async Task Fourier()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).OpenAsync(Audit.Builder());

        var modExpect = new byte[] { 127, 156, 183, 205, 220, 227, 226, 219, 205, 188, 170, 153, 139, 129, 124, 123, 127, 133, 140, 147, 152, 155, 154, 151, 145, 138, 131,
        125, 120, 118, 119, 122, 127, 132, 137, 140, 141, 141, 137, 133, 127, 121, 116, 113, 112, 113, 117, 121, 127, 131, 134, 135, 133, 129,
        122, 115, 108, 102, 99,  99,  101, 106, 113, 120, 127, 130, 129, 124, 115, 100, 83,  65,  48,  35,  27,  26,  34,  48,  70,  97 };

        {
            var m = (new Sine(50) + new Sine(100)).AddComponent(new Sine(150 * Hz))
              .AddComponentsFromIter(new[] { 200 }.Select(x => new Sine(x))) + new Sine(250);
            Assert.Equal(5120u, m.SamplingConfig.FreqDivision);
            Assert.Equal(modExpect.Length, m.Length);
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            Assert.True(await autd.SendAsync(m));
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);

                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(5120u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }


        {
            var m = ((new Sine(50) + new Sine(100)).AddComponent(new Sine(150 * Hz))
              .AddComponentsFromIter(new[] { 200 }.Select(x => new Sine(x))) + new Sine(250)).WithLoopBehavior(LoopBehavior.Once);
            Assert.Equal(5120u, m.SamplingConfig.FreqDivision);
            Assert.Equal(modExpect.Length, m.Length);
            Assert.Equal(LoopBehavior.Once, m.LoopBehavior);
            Assert.True(await autd.SendAsync(m));
            foreach (var dev in autd.Geometry)
            {
                Assert.Equal(LoopBehavior.Once, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(5120u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }
}