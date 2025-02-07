namespace tests.Driver.Datagram;

public class SynchronizeTest
{
    [Fact]
    public void TestSynchronize()
    {
        var autd = CreateController();
        autd.Send(new Synchronize());
    }
}
