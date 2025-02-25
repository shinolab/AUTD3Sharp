namespace tests.Gain;

public class FocusTest
{
    [Fact]
    public void Focus()
    {
        var autd = CreateController();
        var g = new Focus(autd.Center(), new FocusOption { Intensity = new EmitIntensity(0x81) });
        autd.Send(g);
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x81, d));
            Assert.Contains(phases, p => p != 0);
        }
    }

    [Fact]
    public void FocusDefault()
    {
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsBase.AUTDGainFocusIsDefault(new FocusOption().ToNative()));
    }
}