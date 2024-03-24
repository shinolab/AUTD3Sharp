namespace tests.Driver.Datagram.Modulation;

public class TransformTest
{
    [Fact]
    public async Task Transform()
    {
        var autd1 = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).OpenAsync(Audit.Builder());
        var autd2 = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).OpenAsync(Audit.Builder());

        var m = new Sine(150);
        var mt = m.WithTransform((_, v) => new EmitIntensity((byte)(v.Value / 2))).WithLoopBehavior(LoopBehavior.Once);
        Assert.Equal(m.SamplingConfiguration, mt.SamplingConfiguration);
        Assert.Equal(m.Length, mt.Length);
        Assert.Equal(LoopBehavior.Once, mt.LoopBehavior);
        Assert.True(await autd1.SendAsync(m));
        Assert.True(await autd2.SendAsync(mt));
        foreach (var dev in autd1.Geometry)
        {
            var modExpect = autd1.Link.Modulation(dev.Idx, Segment.S0).Select(v => (byte)(v / 2));
            var mod = autd2.Link.Modulation(dev.Idx, Segment.S0);

            Assert.Equal(modExpect, mod);
            Assert.Equal(autd1.Link.ModulationFrequencyDivision(dev.Idx, Segment.S0), autd2.Link.ModulationFrequencyDivision(dev.Idx, Segment.S0));
        }
    }
}
