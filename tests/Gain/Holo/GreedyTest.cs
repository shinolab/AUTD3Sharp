using AUTD3Sharp.Gain.Holo;

namespace tests.Gain.Holo;

public class GreedyTest
{
    [Fact]
    public void Greedy()
    {
        var autd = AUTD3Sharp.Controller.Open([new AUTD3()], new Audit());
        var g = new Greedy(new float[] { -40, 40 }.Select(x => (autd.Center() + new Vector3(x, 0, 150), 5e3f * Pa)), new GreedyOption { EmissionConstraint = EmissionConstraint.Uniform(new EmitIntensity(0x80)) });
        autd.Send(g);
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void GreedyDefault()
    {
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsGainHolo.AUTDGainGreedyIsDefault(new GreedyOption().ToNative()));
    }
}