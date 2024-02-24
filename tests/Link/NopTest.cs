namespace tests.Link;

public class NopTest
{
    [Fact]
    public async Task TestNop()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.zero)).AddDevice(new AUTD3(Vector3d.zero)).OpenAsync(Nop.Builder());

        await autd.CloseAsync();
    }
}
