namespace tests.Link;

public class NopTest
{
    [Fact]
    public async Task TestNop()
    {
        var autd = await new ControllerBuilder().AddDevice(new AUTD3(Vector3.Zero)).AddDevice(new AUTD3(Vector3.Zero)).OpenAsync(Nop.Builder());

        await autd.CloseAsync();
    }
}
