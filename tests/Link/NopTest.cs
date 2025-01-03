namespace tests.Link;

public class NopTest
{
    [Fact]
    public async Task TestNop()
    {
        var autd = await Controller.Builder([new AUTD3(Point3.Origin), new AUTD3(Point3.Origin)]).OpenAsync(Nop.Builder());

        await autd.CloseAsync();
    }
}
