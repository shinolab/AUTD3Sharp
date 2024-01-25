using System.Net;

namespace tests.Link;

public class SimulatorTest
{
    [Fact]
    public void TestSimulator()
    {
        var _ = Simulator.Builder(8080)
                .WithServerIp(IPAddress.Parse("127.0.0.1"))
                .WithTimeout(TimeSpan.FromMilliseconds(200));
    }
}
