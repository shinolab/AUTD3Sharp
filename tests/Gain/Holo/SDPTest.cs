using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Units;

namespace tests.Gain.Holo;

public class SDPTest
{
    [Fact]
    public async Task SDP()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var backend = new NalgebraBackend();
        var g = new SDP<NalgebraBackend>(backend, new float[] { -40, 40 }.Select(x => (autd.Geometry.Center + new Vector3(x, 0, 150), 5e3f * Pa)))
            .WithAlpha(1e-3f)
            .WithLambda(0.9f)
            .WithRepeat(10)
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
    public async Task SDPDefault()
    {
#pragma warning disable CS8602, CS8605
        var autd = await AUTDTest.CreateController();
        var backend = new NalgebraBackend();
        var g = new SDP<NalgebraBackend>(backend, [(autd.Geometry.Center, 5e3f * Pa), (autd.Geometry.Center, 5e3f * Pa)]);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainSDPIsDefault((AUTD3Sharp.NativeMethods.GainPtr)typeof(SDP<NalgebraBackend>).GetMethod("GainPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(g,
            [autd.Geometry])));
#pragma warning restore CS8602, CS8605
    }
}
