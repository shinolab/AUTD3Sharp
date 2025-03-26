namespace tests.Modulation;

public class RadiationPressureTest
{
    [Fact]
    public void RadiationPressure()
    {
        var autd = CreateController();
        var mr = new RadiationPressure(new Sine(freq: 150 * Hz, option: new SineOption()));
        autd.Send(mr);
        var modExpect = new byte[] { 181, 200, 217, 231, 243, 250, 254, 255, 252, 245, 236, 222, 206, 188, 166, 142, 116, 89, 60, 32, 0,  32, 60, 89,  116, 142, 166,
            188, 206, 222, 236, 245, 252, 255, 254, 250, 243, 231, 217, 200, 181, 158, 134, 107, 78, 50, 23, 0,  39, 70, 97,  125, 150, 173,
            194, 212, 227, 239, 248, 253, 255, 253, 248, 239, 227, 212, 194, 173, 150, 125, 97,  70, 39, 0,  23, 50, 78, 107, 134, 158};
        foreach (var dev in autd)
        {
            Assert.Equal(modExpect, autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0));
            Assert.Equal(10u, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
        }
    }

    [Fact]
    public void ExpectedRadiationPressure()
    {
        Assert.Equal(1.0f, new Static(0xFF).ExpectedRadiationPressure());
        Assert.Equal(0.25196465849876404f, new Static(0x80).ExpectedRadiationPressure());
        Assert.Equal(0.503821611404419f, new RadiationPressure(new Static(0x80)).ExpectedRadiationPressure());
    }
}
