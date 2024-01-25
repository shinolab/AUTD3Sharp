using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Gain.Holo.Amplitude.Units;

namespace tests.Gain.Holo;

public class GreedyTest
{
    [Fact]
    public async Task Greedy()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).OpenWithAsync(Audit.Builder());

        var g = new Greedy()
            .AddFocus(autd.Geometry.Center + new Vector3d(30, 0, 150), 5e3 * Pascal)
            .AddFociFromIter(new double[] { -40 }.Select(x => (autd.Geometry.Center + new Vector3d(x, 0, 150), 5e3 * Pascal)))
            .WithPhaseDiv(16)
            .WithConstraint(EmissionConstraint.Uniform(new EmitIntensity(0x80)));

        Assert.True(await autd.SendAsync(g));

        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.IntensitiesAndPhases(dev.Idx, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }
}