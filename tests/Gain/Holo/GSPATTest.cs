using AUTD3Sharp.Gain.Holo;

namespace tests.Gain.Holo;

public class GSPATTest
{
    [Fact]
    public void GSPAT()
    {
        var autd = CreateController(1);
        var backend = new NalgebraBackend();
        var g = new GSPAT(new float[] { -40, 40 }.Select(x => (autd.Center() + new Vector3(x, 0, 150), 5e3f * Pa)), new GSPATOption { EmissionConstraint = EmissionConstraint.Uniform(new EmitIntensity(0x80)) }, backend);
        autd.Send(g);
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void GSPATDefault()
    {
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainGSPATIsDefault(new GSPATOption().ToNative()));
    }
}
