namespace tests.Gain;

public class PlaneTest
{
    [Fact]
    public void Plane()
    {
        var autd = CreateController();

        autd.Send(new Plane(new Vector3(0, 0, 1), new PlaneOption
        {
            Intensity = new EmitIntensity(0x80),
            PhaseOffset = new Phase(0x81),
        }));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x81, p));
        }
    }

    [Fact]
    public void PlaneDefault()
    {
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDGainPlanelIsDefault(new PlaneOption().ToNative()));
    }
}
