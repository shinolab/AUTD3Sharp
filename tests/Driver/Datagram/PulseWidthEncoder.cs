namespace tests.Driver.Datagram;

public class PulseWidthEncoderTest
{
    [Fact]

    public async Task PulseWidthEncoder()
    {
        var autd = await AUTDTest.CreateController();

        var rnd = new Random();
        var buf = Enumerable.Range(0, 32768).Select(_ => (ushort)rnd.Next(0, 257)).OrderBy(i => i).ToArray();

        await autd.SendAsync(new AUTD3Sharp.PulseWidthEncoder(dev => i => buf[i]));
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(buf, autd.Link.PulseWidthEncoderTable(dev.Idx));
        }
    }

    [Fact]

    public async Task PulseWidthEncoderDefault()
    {
        var autd = await AUTDTest.CreateController();

        var buf = Enumerable.Range(0, 255 * 255 / 2).Select(i =>
            (ushort)Math.Round(Math.Asin((float)i / (255 * 255 / 2)) / MathF.PI * 512)
        ).Concat(Enumerable.Repeat((ushort)256, 32768 - 255 * 255 / 2)).ToArray();

        await autd.SendAsync(new AUTD3Sharp.PulseWidthEncoder());
        foreach (var dev in autd.Geometry)
        {
            Assert.Equal(buf, autd.Link.PulseWidthEncoderTable(dev.Idx));
        }
    }
}