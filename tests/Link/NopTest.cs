namespace tests.Link;

public class NopTest
{
    [Fact]
    public void TestNop()
    {
        var autd = AUTD3Sharp.Controller.Open([new AUTD3(), new AUTD3()], new Nop());

        autd.Close();
    }
}
