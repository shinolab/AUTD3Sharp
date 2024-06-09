namespace tests.Link;

public class NopTest
{
    [Fact]
    public async Task TestNop()
    {
        var autd = await new ControllerBuilder([new AUTD3(Vector3.Zero), new AUTD3(Vector3.Zero)]).OpenAsync(Nop.Builder());

        await autd.CloseAsync();
    }
}
