using AUTD3Sharp.Driver.Geometry;

namespace tests.Gain;

public class GainTest
{
    public class MyUniform(EmitIntensity intensity, Phase phase, bool[] check) : AUTD3Sharp.Gain.Gain
    {
        public bool[] Check = check;

        public override Dictionary<int, Drive[]> Calc(Geometry geometry)
        {
            return Transform(geometry, (dev, _) =>
            {
                Check[dev.Idx] = true;
                return new Drive { Phase = phase, Intensity = intensity };
            });
        }
    }

    [Fact]
    public async Task Gain()
    {
        var autd = await AUTDTest.CreateController();

        var check = new bool[autd.Geometry.NumDevices];
        Assert.True(await autd.SendAsync(new MyUniform(new EmitIntensity(0x80), new Phase(0x90), check)));

        foreach (var dev in autd.Geometry)
        {
            var (intensities, phases) = autd.Link.Drives(dev.Idx, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }
    }

    [Fact]
    public async Task GainCheckOnlyForEnabled()
    {
        var autd = await AUTDTest.CreateController();
        autd.Geometry[0].Enable = false;

        var check = new bool[autd.Geometry.NumDevices];
        Assert.True(await autd.SendAsync(new MyUniform(new EmitIntensity(0x80), new Phase(0x90), check)));

        Assert.False(check[0]);
        Assert.True(check[1]);

        {
            var (intensities, phases) = autd.Link.Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, phases) = autd.Link.Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }
    }
}