namespace tests.Gain;

public class BesselTest
{
    [Fact]
    public void Bessel()
    {
        var autd = CreateController();

        var g = new Bessel(autd.Center(), new Vector3(0, 0, 1), MathF.PI / 4 * rad, new BesselOption
        {
            Intensity = new EmitIntensity(0x80)
        });
        autd.Send(g);
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void BesselDefault()
    {
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDGainBesselIsDefault(new BesselOption().ToNative()));
    }
}
