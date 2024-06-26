namespace tests.Driver.Datagram.Modulation;

public class TransformTest
{
    [Fact]
    public async Task Transform()
    {
        var autd1 = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());
        var autd2 = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var m = new Sine(150 * Hz);
        var mt = m.WithTransform((_, v) => (byte)(v / 2)).WithLoopBehavior(LoopBehavior.Once);
        Assert.Equal(LoopBehavior.Once, mt.LoopBehavior);
        await autd1.SendAsync(m);
        await autd2.SendAsync(mt);
        foreach (var dev in autd1.Geometry)
        {
            var modExpect = autd1.Link.Modulation(dev.Idx, Segment.S0).Select(v => (byte)(v / 2));
            var mod = autd2.Link.Modulation(dev.Idx, Segment.S0);

            Assert.Equal(modExpect, mod);
            Assert.Equal(autd1.Link.ModulationFreqDivision(dev.Idx, Segment.S0), autd2.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
        }
    }
}
