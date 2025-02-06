namespace tests.Driver.Datagram.Modulation;

public class CacheTest
{
    [Fact]
    public void Cache()
    {
        var autd = CreateController();
        autd.Send(new AUTD3Sharp.Modulation.Cache(new AUTD3Sharp.Modulation.Custom([0x80, 0x81], new SamplingConfig(0xFFFF))));
        foreach (var dev in autd)
        {
            Assert.Equal([0x80, 0x81], autd.Link().Modulation(dev.Idx(), Segment.S0));
            Assert.Equal(0xFFFF, autd.Link().ModulationFreqDivision(dev.Idx(), Segment.S0));
        }
    }
}
