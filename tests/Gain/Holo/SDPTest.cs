using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Gain.Holo.Amplitude.Units;

namespace tests.Gain.Holo;

public class SDPTest
{
    [Fact]
    public async Task SDP()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).OpenWithAsync(Audit.Builder());

        var backend = new NalgebraBackend();
        var g = new SDP<NalgebraBackend>(backend)
            .AddFocus(autd.Geometry.Center + new Vector3d(30, 0, 150), 5e3 * Pascal)
            .AddFociFromIter(new double[] { -40 }.Select(x => (autd.Geometry.Center + new Vector3d(x, 0, 150), 5e3 * Pascal)))
            .WithAlpha(1e-3)
            .WithLambda(0.9)
            .WithRepeat(10)
            .WithConstraint(EmissionConstraint.Uniform(0x80));

        Assert.True(await autd.SendAsync(g));

        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
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
        var g = new SDP<NalgebraBackend>(backend);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainSDPIsDefault((AUTD3Sharp.NativeMethods.GainPtr)typeof(SDP<NalgebraBackend>).GetMethod("GainPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(g, new object[] { autd.Geometry })));
#pragma warning restore CS8602, CS8605
    }
}
