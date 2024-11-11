
namespace tests;

public class DurationTests
{
    [Fact]
    public void TestDuration()
    {
        var d = Duration.FromNanos(1);
        Assert.Equal(1ul, d.AsNanos());

        d = Duration.FromMicros(1);
        Assert.Equal(1000ul, d.AsNanos());
        Assert.Equal(1ul, d.AsMicros());

        d = Duration.FromMillis(1);
        Assert.Equal(1000000ul, d.AsNanos());
        Assert.Equal(1000ul, d.AsMicros());
        Assert.Equal(1ul, d.AsMillis());

        d = Duration.FromSecs(1);
        Assert.Equal(1000000000ul, d.AsNanos());
        Assert.Equal(1000000ul, d.AsMicros());
        Assert.Equal(1000ul, d.AsMillis());
        Assert.Equal(1ul, d.AsSecs());

        var d1 = Duration.FromMicros(1);
        var d2 = Duration.FromMicros(2);
        Assert.Equal(3ul, (d1 + d2).AsMicros());
        Assert.Equal(1ul, (d2 - d1).AsMicros());
        Assert.Equal(2ul, (d1 * 2).AsMicros());
        Assert.Equal(2ul, (2 * d1).AsMicros());
        Assert.Equal(1ul, (d2 / 2).AsMicros());
        Assert.Equal(500ul, (d1 / 2).AsNanos());
        Assert.Equal(100ul, (d1 % 300).AsNanos());

        Assert.False(d1.Equals(1000));
        Assert.True(d1.Equals((object?)Duration.FromNanos(1000)));
        Assert.False(d1 != Duration.FromNanos(1000));
        Assert.True(d1 == Duration.FromNanos(1000));
        Assert.False(d1 == d2);
        Assert.True(d1 < d2);
        Assert.True(d1 <= d2);
        Assert.True(d2 > d1);
        Assert.True(d2 >= d1);

        Assert.Equal(d1.GetHashCode(), 1000ul.GetHashCode());
        Assert.Equal("1ns", Duration.FromNanos(1).ToString());
        Assert.Equal("1.001μs", Duration.FromNanos(1001).ToString());
        Assert.Equal("1.1μs", Duration.FromNanos(1100).ToString());
        Assert.Equal("1μs", Duration.FromMicros(1).ToString());
        Assert.Equal("1.001ms", Duration.FromMicros(1001).ToString());
        Assert.Equal("1.1ms", Duration.FromMicros(1100).ToString());
        Assert.Equal("1ms", Duration.FromMillis(1).ToString());
        Assert.Equal("1.001s", Duration.FromMillis(1001).ToString());
        Assert.Equal("1.1s", Duration.FromMillis(1100).ToString());
        Assert.Equal("1s", Duration.FromSecs(1).ToString());
    }
}
