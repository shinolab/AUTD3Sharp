using static AUTD3Sharp.Units;

namespace tests.Gain.Holo;

public class AmplitudeTest
{
    [Fact]
    public void HoloAmplitudedB()
    {
        var amp = 121.5f * dB;
        Assert.Equal(23.7700348f, amp.Pascal);
        Assert.Equal(121.5f, amp.SPL);
    }

    [Fact]
    public void HoloAmplitudePascal()
    {
        var amp = 23.7700348f * Pa;
        Assert.Equal(23.7700348f, amp.Pascal);
        Assert.Equal(121.5f, amp.SPL);
    }
}