using AUTD3Sharp.Gain.Holo;

namespace tests.Gain.Holo;

public class GSPATTest
{
    [Fact]
    public void GSPAT()
    {
        var autd = Controller.Builder([new AUTD3(Point3.Origin)]).Open(Audit.Builder());

        var backend = new NalgebraBackend();
        var g = new GSPAT(backend, new float[] { -40, 40 }.Select(x => (autd.Center + new Vector3(x, 0, 150), 5e3f * Pa)))
            .WithRepeat(100)
            .WithConstraint(EmissionConstraint.Uniform(new EmitIntensity(0x80)));

        autd.Send(g);

        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void GSPATDefault()
    {
        var backend = new NalgebraBackend();
        var g = new GSPAT(backend, [(Point3.Origin, 5e3f * Pa), (Point3.Origin, 5e3f * Pa)]);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainGSPATIsDefault(
            g.Constraint, g.Repeat));
    }
}