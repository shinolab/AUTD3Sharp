using System.Net;

namespace tests.Link;

public class SimulatorTest
{
    [Fact]
    public void TestSimulator()
    {
        _ = new Simulator(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
    }
}
