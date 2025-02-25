using AUTD3Sharp;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.Gain;
using AUTD3Sharp.Modulation;
using AUTD3Sharp.Utils;
using static AUTD3Sharp.Units;

namespace Samples;

internal static class GroupByDeviceTest
{
    public static void Test(Controller autd)
    {
        var config = new Silencer();
        autd.Send(config);

        autd.GroupSend(dev => dev.Idx() switch
        {
            0 => "null",
            1 => "focus",
            _ => null
        }, new GroupDictionary()
            {
                { "null", (new Static(), new Null()) },
                { "focus", (new Sine(freq:150 * Hz, option: new SineOption()), new Focus(pos: autd.Center() + new Vector3(0, 0, 150), option: new FocusOption())) }
            });
    }
}


internal static class GroupByTransducerTest
{
    public static void Test(Controller autd)
    {
        var config = new Silencer();
        autd.Send(config);

        var cx = autd.Center().X;
        var g1 = new Focus(pos: autd.Center() + new Vector3(0, 0, 150), option: new FocusOption());
        var g2 = new Null();

        var g = new Group(
            _ => tr => tr.Position().X < cx ? "focus" : "null",
            new Dictionary<object, IGain>
            {
                { "focus", g1 },
                { "null", g2 }
            });

        var m = new Sine(freq: 150 * Hz, option: new SineOption());

        autd.Send((m, g));
    }
}
