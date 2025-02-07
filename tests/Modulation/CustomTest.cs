namespace tests.Modulation;

public class CustomTest
{
    [Fact]
    public void ModulationCustom()
    {
        var autd = CreateController(1);

        var modExpect = new byte[] { 255, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        autd.Send(new AUTD3Sharp.Modulation.Custom(buffer: modExpect, config: new SamplingConfig(10)));
        foreach (var dev in autd)
        {
            var mod = autd.Link().Modulation(dev.Idx(), Segment.S0);
            Assert.Equal(modExpect, mod);
            Assert.Equal(10, autd.Link().ModulationFreqDivision(dev.Idx(), Segment.S0));
        }
    }
}
