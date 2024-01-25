using System.Net;

namespace tests.Link;

public class TwinCATTest
{
    [Fact]
    public void TestTwinCAT()
    {
        var _ = TwinCAT.Builder().WithTimeout(TimeSpan.FromMilliseconds(200));
    }

    [Fact]
    public void TestRemoteTwinCAT()
    {
        var _ = RemoteTwinCAT.Builder("xxx.xxx.xxx.xxx.xxx.xxx")
                .WithServerIp(IPAddress.Parse("127.0.0.1"))
                .WithClientAmsNetId("xxx.xxx.xxx.xxx.xxx.xxx")
                .WithTimeout(TimeSpan.FromMilliseconds(200));
    }
}
