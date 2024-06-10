using AUTD3Sharp.Modulation.AudioFile;

namespace tests.Modulation.AudioFile;

public class RawPCMTest
{
    [Fact]
    public async Task RawPCM()
    {
        var autd = await Controller.Builder([new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());

        var modExpect = new byte[] {
                157,
                185,
                210,
                231,
                245,
                253,
                255,
                249,
                236,
                218,
                194,
                167,
                138,
                108,
                79,
                53,
                31,
                14,
                4,
                0,
                4,
                14,
                31,
                53,
                79,
                108,
                138,
                167,
                194,
                218,
                236,
                249,
                255,
                253,
                245,
                231,
                210,
                185,
                157,
                128,
                98,
                70,
                45,
                24,
                10,
                2,
                0,
                6,
                19,
                37,
                61,
                88,
                117,
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
                117,
                88,
                61,
                37,
                19,
                6,
                0,
                2,
                10,
                24,
                45,
                70,
                98,
                128};

        {
            var m = new RawPCM("sin150.dat", 4000 * Hz);
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
            var m = new RawPCM("sin150.dat", 2000 * Hz).WithLoopBehavior(LoopBehavior.Once);
            Assert.Equal(LoopBehavior.Once, m.LoopBehavior);
            await autd.SendAsync(m);
            foreach (var dev in autd.Geometry)
            {
                Assert.Equal(LoopBehavior.Once, autd.Link.ModulationLoopBehavior(dev.Idx, Segment.S0));
                Assert.Equal(10240u, autd.Link.ModulationFreqDivision(dev.Idx, Segment.S0));
            }
        }
    }
}
