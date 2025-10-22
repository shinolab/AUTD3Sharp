using System.Net;

namespace tests;

public class RemoteTest
{

    [Fact]
    public void TestOption()
    {
        {
            var option = new RemoteOption();
            var native = option.ToNative();
            Assert.False(native.timeout.has_value);
        }

        {
            var option = new RemoteOption
            {
                Timeout = Duration.FromNanos(0),
            };
            var native = option.ToNative();
            Assert.True(native.timeout.has_value);
            Assert.Equal(0ul, native.timeout.value.AsNanos());
        }
    }

    [Fact]
    public void TestRemote()
    {
        _ = new Remote(new IPEndPoint(IPAddress.Parse("172.0.0.1"), 8080), new RemoteOption());
    }
}