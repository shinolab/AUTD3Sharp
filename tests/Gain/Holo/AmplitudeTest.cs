namespace tests.Gain.Holo;

public class AmplitudeTest
{
    [Fact]
    public void HoloAmplitudedB()
    {
        var amp = 121.5f * dB;
        Assert.Equal(23.7700348f, amp.Pascal());
        Assert.Equal(121.5f, amp.SPL());
    }

    [Fact]
    public void HoloAmplitudePascal()
    {
        var amp = 23.7700348f * Pa;
        Assert.Equal(23.7700348f, amp.Pascal());
        Assert.Equal(121.5f, amp.SPL());
    }

    [Fact]
    public void Equals_Amplitude()
    {
        var m1 = 0f * Pa;
        var m2 = 0f * Pa;
        var m3 = 1f * Pa;

        Assert.True(m1 == m2);
        Assert.True(m1 != m3);
        Assert.True(!m1.Equals(null));
        Assert.True(m1.Equals((object?)m2));
        Assert.True(!m1.Equals((object?)null));
    }
}
