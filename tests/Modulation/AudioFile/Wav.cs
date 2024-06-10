using AUTD3Sharp.Modulation.AudioFile;

namespace tests.Modulation.AudioFile;

public class WavTest
{
    [Fact]
    public async Task Wav()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

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
            Assert.Equal(LoopBehavior.Infinite, m.LoopBehavior);
            await autd.SendAsync(m);
            foreach (var dev in autd.Geometry)
            {
                var mod = autd.Link.Modulation(dev.Idx, Segment.S0);
                Assert.Equal(modExpect, mod);
                Assert.Equal(LoopBehavior.Infinite, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(5120u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }

        {
            var m = new Wav("sin150.wav").WithSamplingConfig(SamplingConfig.Division(10240)).WithLoopBehavior(LoopBehavior.Once);
            Assert.Equal(LoopBehavior.Once, m.LoopBehavior);
            await autd.SendAsync(m);
            foreach (var dev in autd.Geometry)
            {
                Assert.Equal(LoopBehavior.Once, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10240u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }

    [Fact]
    public void WavDefault()
    {
#pragma warning disable CS8602, CS8605
        var m = new Wav(" ");
        var autd = AUTDTest.CreateControllerSync();
        Assert.True(AUTD3Sharp.NativeMethods.NativeMethodsModulationAudioFile.AUTDModulationWavIsDefault((AUTD3Sharp.NativeMethods.ModulationPtr)typeof(Wav).GetMethod("ModulationPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(m,
            [autd.Geometry])));
#pragma warning restore CS8602, CS8605
    }
}
