using AUTD3Sharp.Modulation.AudioFile;

namespace tests.Modulation.AudioFile;

public class CsvTest
{
    [Fact]
    public void Csv()
    {
        var autd = CreateController(1);

        var modExpect = new byte[] {
               157, 185, 210, 231, 245, 253, 255, 249, 236, 218, 194, 167, 138, 108, 79,  53,  31,  14,  4,   0,
                                    4,   14,  31,  53,  79,  108, 138, 167, 194, 218, 236, 249, 255, 253, 245, 231, 210, 185, 157, 128,
                                    98,  70,  45,  24,  10,  2,   0,   6,   19,  37,  61,  88,  117, 147, 176, 202, 224, 241, 251, 255,
                                    251, 241, 224, 202, 176, 147, 117, 88,  61,  37,  19,  6,   0,   2,   10,  24,  45,  70,  98,  128};

        var m = new Csv("sin150.csv", 4000f * Hz, new CsvOption());
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
