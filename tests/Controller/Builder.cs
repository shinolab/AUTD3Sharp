using AUTD3Sharp.NativeMethods;

namespace tests.ControllerTest;

public class ControllerBuilderTest
{
    [Fact]
    public void OpenWithTimeout()
    {
        _ = Controller.Builder([new AUTD3(Vector3.Zero)]).Open(Audit.Builder(), TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public async Task OpenWithTimeoutAsync()
    {
        _ = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder(), TimeSpan.FromMilliseconds(1));
    }
}
