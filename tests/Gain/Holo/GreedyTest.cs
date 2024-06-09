using AUTD3Sharp.Gain.Holo;
using static AUTD3Sharp.Units;

namespace tests.Gain.Holo;

public class GreedyTest
{
    [Fact]
    public async Task Greedy()
    {
        var autd = await new ControllerBuilder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var g = new Greedy()
            .AddFocus(autd.Geometry.Center + new Vector3(30, 0, 150), 5e3f * Pa)
            .AddFociFromIter(new float[] { -40 }.Select(x => (autd.Geometry.Center + new Vector3(x, 0, 150), 5e3f * Pa)))
            .WithPhaseDiv(16)
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
    public async Task GreedyDefault()
    {
#pragma warning disable CS8602, CS8605
        var autd = await AUTDTest.CreateController();
        var g = new Greedy();
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainGreedyIsDefault((AUTD3Sharp.NativeMethods.GainPtr)typeof(Greedy).GetMethod("GainPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(g,
            [autd.Geometry])));
#pragma warning restore CS8602, CS8605
    }
}