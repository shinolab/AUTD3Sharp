using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Units;

namespace tests.Gain.Holo;

public class GSTest
{
    [Fact]
    public async Task GS()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var backend = new NalgebraBackend();
        var g = new GS(backend, new float[] { -40, 40 }.Select(x => (autd.Geometry.Center + new Vector3(x, 0, 150), 5e3f * Pa)))
            .WithRepeat(100)
            .WithConstraint(EmissionConstraint.Uniform(new EmitIntensity(0x80)));

        await autd.SendAsync(g);

        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void GSDefault()
    {
        var backend = new NalgebraBackend();
        var g = new GS(backend, [(Vector3.Zero, 5e3f * Pa), (Vector3.Zero, 5e3f * Pa)]);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainGSIsDefault(
            g.Constraint, g.Repeat
            ));
    }
}
