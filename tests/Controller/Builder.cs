using AUTD3Sharp.NativeMethods;

namespace tests.ControllerTest;

public class ControllerBuilderTest
{
    [Fact]
    public void OpenWithTimeout()
    {
        using var autd = Controller.Builder([new AUTD3(Vector3.Zero)]).Open(Audit.Builder(), TimeSpan.FromMilliseconds(1));

        Assert.Equal(40000u, autd.Link.UltrasoundFreq(0));
    }

    [Fact]
    public async Task OpenWithTimeoutAsync()
    {
        using var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder(), TimeSpan.FromMilliseconds(1));

        Assert.Equal(40000u, autd.Link.UltrasoundFreq(0));
    }
}
