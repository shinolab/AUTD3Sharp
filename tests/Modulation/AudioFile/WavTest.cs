using AUTD3Sharp.Modulation.AudioFile;

namespace tests.Modulation.AudioFile;

public class WavTest
{
    [Fact]
    public void Wav()
    {
        var autd = CreateController(1);

        var modExpect = new byte[] {
                128,
                157,
                185,
                210,
                230,
                245,
                253,
                254,
                248,
                236,
                217,
                194,
                167,
                137,
                109,
                80,
                54,
                32,
                15,
                5,
                1,
                5,
                15,
                32,
                54,
                80,
                109,
                137,
                167,
                194,
                217,
                236,
                248,
                254,
                253,
                245,
                230,
                210,
                185,
                157,
                128,
                99,
                71,
                46,
                26,
                11,
                3,
                2,
                8,
                20,
                39,
                62,
                89,
                119,
                147,
                176,
                202,
                224,
                241,
                251,
                255,
                251,
                241,
                224,
                202,
                176,
                147,
                119,
                89,
                62,
                39,
                20,
                8,
                2,
                3,
                11,
                26,
                46,
                71,
                99};

        {
            var m = new Wav("sin150.wav");
            autd.Send(m);
            foreach (var dev in autd)
            {
                var mod = autd.Link<Audit>().Modulation(dev.Idx(), Segment.S0);
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link<Audit>().ModulationLoopBehavior(dev.Idx(), Segment.S0));
                Assert.Equal(10u, autd.Link<Audit>().ModulationFreqDivision(dev.Idx(), Segment.S0));
            }
        }
    }
}
