using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Units;

namespace tests.Gain.Holo;

public class GreedyTest
{
    [Fact]
    public async Task Greedy()
    {
        var autd = await Controller.Builder([new AUTD3(Point3.Origin)]).OpenAsync(Audit.Builder());

        var g = new Greedy(new float[] { -40, 40 }.Select(x => (autd.Center + new Vector3(x, 0, 150), 5e3f * Pa)))
            .WithPhaseDiv(16)
            .WithConstraint(EmissionConstraint.Uniform(new EmitIntensity(0x80)));

        await autd.SendAsync(g);

        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void GreedyDefault()
    {
        var g = new Greedy([(Point3.Origin, 5e3f * Pa), (Point3.Origin, 5e3f * Pa)]);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainGreedyIsDefault(
            g.Constraint,
            g.PhaseDiv
            ));
    }
}