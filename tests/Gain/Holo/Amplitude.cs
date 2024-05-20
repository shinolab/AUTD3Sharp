using static AUTD3Sharp.Units;

namespace tests.Gain.Holo;

public class AmplitudeTest
{
    [Fact]
    public void HoloAmplitudedB()
    {
        var amp = 121.5 * dB;
        Assert.Equal(23.77004454874038, amp.Pascal);
        Assert.Equal(121.5, amp.SPL);
    }

    [Fact]
    public void HoloAmplitudePascal()
    {
        var amp = 23.77004454874038 * Pa;
        Assert.Equal(23.77004454874038, amp.Pascal);
        Assert.Equal(121.5, amp.SPL);
    }
}