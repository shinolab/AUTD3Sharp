namespace tests.Gain;

public class BesselTest
{
    [Fact]
    public void Bessel()
    {
        var autd = AUTDTest.CreateController();

        var g = new Bessel(autd.Center, new Vector3(0, 0, 1), MathF.PI / 4 * rad).WithIntensity(0x80).WithPhaseOffset(0x81);
        Assert.Equal(autd.Center, g.Pos);
        Assert.Equal(new Vector3(0, 0, 1), g.Dir);
        Assert.Equal(MathF.PI / 4 * rad, g.Theta);
        Assert.Equal(0x80, g.Intensity.Value);
        Assert.Equal(0x81, g.PhaseOffset.Value);
        autd.Send(g);
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void BesselDefault()
    {
        var g = new Bessel(Point3.Origin, Vector3.Zero, 0 * rad);
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDGainBesselIsDefault(g.Intensity.Value, g.PhaseOffset.Value));
    }
}
