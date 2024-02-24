namespace tests.Modulation;

public class RadiationPressureTest
{
    [Fact]
    public async Task RadiationPressure()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).OpenAsync(Audit.Builder());

        Assert.True(await autd.SendAsync(new Sine(150).WithRadiationPressure()));
        foreach (var dev in autd.Geometry)
        {
            var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
            var modExpect = new byte[] { 180, 200, 217, 231, 242, 250, 254, 254, 251, 245, 235, 222, 206, 187, 165, 141, 115, 87, 58, 28, 0,  28, 58, 87,  115, 141, 165,
        187, 206, 222, 235, 245, 251, 254, 254, 250, 242, 231, 217, 200, 180, 157, 133, 106, 78, 48, 16, 0,  39, 68, 97,  124, 150, 173,
        194, 212, 227, 239, 248, 253, 255, 253, 248, 239, 227, 212, 194, 173, 150, 124, 97,  68, 39, 0,  16, 48, 78, 106, 133, 157};
            Assert.Equal(modExpect, mod);
            Assert.Equal(5120u, autd.Link.ModulationFrequencyDivision(dev.Idx, Segment.S0));
        }
    }
}
