using System.Net;

namespace tests.Link;

public class SimulatorTest
{
    [Fact]
    public void TestSimulator()
    {
        _ = Simulator.Builder(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080))
            .WithTimeout(TimeSpan.FromMilliseconds(200));
    }
}
