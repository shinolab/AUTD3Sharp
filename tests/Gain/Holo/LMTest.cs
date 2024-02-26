using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Gain.Holo.Amplitude.Units;

namespace tests.Gain.Holo;

public class LMTest
{
    [Fact]
    public async Task LM()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).OpenAsync(Audit.Builder());

        var backend = new NalgebraBackend();
        var g = new LM<NalgebraBackend>(backend)
            .AddFocus(autd.Geometry.Center + new Vector3d(30, 0, 150), 5e3 * Pascal)
            .AddFociFromIter(new double[] { -40 }.Select(x => (autd.Geometry.Center + new Vector3d(x, 0, 150), 5e3 * Pascal)))
            .WithEps1(1e-3)
            .WithEps2(1e-3)
            .WithTau(1e-3)
            .WithKMax(5)
            .WithInitial([1.0])
            .WithConstraint(EmissionConstraint.Uniform(0x80));
        Assert.Equal(1e-3, g.Eps1);
        Assert.Equal(1e-3, g.Eps2);
        Assert.Equal(1e-3, g.Tau);
        Assert.Equal(5u, g.KMax);
        Assert.Equal([1.0], g.Initial.ToArray());
        Assert.True(await autd.SendAsync(g));

        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public async Task LMDefault()
    {
#pragma warning disable CS8602, CS8605
        var autd = await AUTDTest.CreateController();
        var backend = new NalgebraBackend();
        var g = new LM<NalgebraBackend>(backend);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainLMIsDefault((AUTD3Sharp.NativeMethods.GainPtr)typeof(LM<NalgebraBackend>).GetMethod("GainPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(g, new object[] { autd.Geometry })));
#pragma warning restore CS8602, CS8605
    }
}
