using AUTD3Sharp.NativeMethods;

namespace tests.Driver.Controller;

public class ControllerBuilderTest
{
    [Fact]
    public void OpenWithTimeout()
    {
        _ = new ControllerBuilder().AddDevice(new AUTD3(Vector3.Zero)).Open(Audit.Builder(), TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public async Task OpenWithTimeoutAsync()
    {
        _ = await new ControllerBuilder().AddDevice(new AUTD3(Vector3.Zero)).OpenAsync(Audit.Builder(), TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public void WithUltrasoundFreq()
    {
        var autd = new ControllerBuilder().AddDevice(new AUTD3(Vector3.Zero)).WithUltrasoundFreq(41 * kHz).Open(Audit.Builder());

        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(41000u, autd.Link.UltrasoundFreq(dev.Idx));
        }
    }
}
