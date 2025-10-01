using System.Net;

namespace tests;

public class RemoteTest
{
    [Fact]
    public void TestRemote()
    {
        _ = new Remote(new IPEndPoint(IPAddress.Parse("172.0.0.1"), 8080));
    }
}