using AUTD3Sharp.Driver.Datagram;

namespace tests.Gain;

public class GroupTest
{
    [Fact]
    public void Group()
    {
        var autd = CreateController();

        var cx = autd.Center().X;

        autd.Send(new Group(
            keyMap: _ => tr => tr.Position().X switch
            {
                var x when x < cx => "uniform",
                _ => "null"
            },
            gainMap: new Dictionary<object, IGain>
            {
                { "uniform", new Uniform(intensity: new EmitIntensity(0x80), phase: new Phase(0x90)) }, { "null", new Null() }
            }));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            foreach (var tr in dev)
            {
                if (tr.Position().X < cx)
                {
                    Assert.Equal(0x80, intensities[tr.Idx()]);
                    Assert.Equal(0x90, phases[tr.Idx()]);
                }
                else
                {
                    Assert.Equal(0, intensities[tr.Idx()]);
                    Assert.Equal(0, phases[tr.Idx()]);
                }
            }
        }

        autd.Send(new Group(keyMap: _ => tr => tr.Position().X switch
            {
                var x when x > cx => "uniform",
                _ => null
            },
            new Dictionary<object, IGain>
            {
                {"uniform", new Uniform(intensity: new EmitIntensity(0x81), phase: new Phase(0x91))}
            }
            ));
        foreach (var dev in autd)
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(dev.Idx(), Segment.S0, 0);
            foreach (var tr in dev)
            {
                if (tr.Position().X > cx)
                {
                    Assert.Equal(0x81, intensities[tr.Idx()]);
                    Assert.Equal(0x91, phases[tr.Idx()]);
                }
                else
                {
                    Assert.Equal(0, intensities[tr.Idx()]);
                    Assert.Equal(0, phases[tr.Idx()]);
                }
            }
        }
    }

    [Fact]
    public void GroupUnknownKey()
    {
        var autd = CreateController();

        var exception = Record.Exception(() =>
        {
            autd.Send(new Group(_ => _ => "null", new Dictionary<object, IGain>()
            {
                {"null", new Null()},
                {"uniform", new Uniform(intensity: new EmitIntensity(0x80), phase: new Phase(0x90))}
            }
            ));
        });

        if (exception == null) Assert.Fail("Exception is expected");
        Assert.Equal(typeof(AUTDException), exception.GetType());
        Assert.Equal("Unknown group key", exception.Message);
    }

    [Fact]
    public void GroupCheckOnlyForEnabled()
    {
        var autd = CreateController();
        var check = new bool[autd.NumDevices()];

        autd[0].Enable = false;

        autd.Send(new Group(dev => _ =>
            {
                check[dev.Idx()] = true;
                return "uniform";
            }, new Dictionary<object, IGain> { { "uniform", new Uniform(intensity: new EmitIntensity(0x80), phase: new Phase(0x90)) } }));

        Assert.False(check[0]);
        Assert.True(check[1]);

        {
            var (intensities, phases) = autd.Link<Audit>().Drives(0, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0, d));
            Assert.All(phases, p => Assert.Equal(0, p));
        }
        {
            var (intensities, phases) = autd.Link<Audit>().Drives(1, Segment.S0, 0);
            Assert.All(intensities, d => Assert.Equal(0x80, d));
            Assert.All(phases, p => Assert.Equal(0x90, p));
        }
    }
}
