using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Units;

namespace tests.Gain.Holo;

public class LMTest
{
    [Fact]
    public async Task LM()
    {
        var autd = await Controller.Builder([new AUTD3(Point3.Origin)]).OpenAsync(Audit.Builder());

        var backend = new NalgebraBackend();
        var g = new LM(backend, new float[] { -40, 40 }.Select(x => (autd.Center + new Vector3(x, 0, 150), 5e3f * Pa)))
            .WithEps1(1e-3f)
            .WithEps2(1e-3f)
            .WithTau(1e-3f)
            .WithKMax(5)
            .WithInitial([1.0f])
            .WithConstraint(EmissionConstraint.Uniform(new EmitIntensity(0x80)));
        Assert.Equal(1e-3f, g.Eps1);
        Assert.Equal(1e-3f, g.Eps2);
        Assert.Equal(1e-3f, g.Tau);
        Assert.Equal(5u, g.KMax);
        Assert.Equal([1.0f], g.Initial.ToArray());
        await autd.SendAsync(g);

        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void LMDefault()
    {
        var backend = new NalgebraBackend();
        var g = new LM(backend, [(Point3.Origin, 5e3f * Pa), (Point3.Origin, 5e3f * Pa)]);
        var initial = g.Initial;
        unsafe
        {
            fixed (float* p = initial)
                Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainLMIsDefault(
                    g.Constraint, g.Eps1, g.Eps2, g.Tau, g.KMax,
        p, (uint)initial.Length
                ));

        }

    }
}
