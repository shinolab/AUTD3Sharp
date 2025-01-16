namespace tests.Gain;

public class PlaneTest
{
    [Fact]
    public void Plane()
    {
        var autd = AUTDTest.CreateController();

        autd.Send(new Plane(new Vector3(0, 0, 1)).WithIntensity(0x80).WithPhaseOffset(0x81));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x81, p));
        }

        autd.Send(new Plane(new Vector3(0, 0, 1)).WithIntensity(0x81));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x81, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
    }

    [Fact]
    public void PlaneDefault()
    {
        var g = new Plane(new Vector3(0, 0, 0));
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDGainPlanelIsDefault(
            g.Intensity.Value, g.PhaseOffset.Value
            ));
    }
}
