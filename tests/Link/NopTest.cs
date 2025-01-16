namespace tests.Link;

public class NopTest
{
    [Fact]
    public void TestNop()
    {
        var autd = Controller.Builder([new AUTD3(Point3.Origin), new AUTD3(Point3.Origin)]).Open(Nop.Builder());

        autd.Close();
    }
}
