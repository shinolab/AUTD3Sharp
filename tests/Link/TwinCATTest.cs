namespace tests.Link;

public class TwinCATTest
{
    [Fact]
    public void TestTwinCAT()
    {
        _ = new TwinCAT();
    }

    [Fact]
    public void TestRemoteTwinCAT()
    {
        _ = new RemoteTwinCAT("xxx.Xxx.Xxx.Xxx.Xxx.Xxx", new RemoteTwinCATOption
        {
            ServerIp = "127.0.0.1",
            ClientAmsNetId = "xxx.Xxx.Xxx.Xxx.Xxx.Xxx"
        });
    }
}
