
using AUTD3Sharp;
using AUTD3Sharp.Gain;

namespace Samples;

internal delegate void TestFn(Controller autd);

public class SampleRunner
{
    public static void Run(Controller autd)
    {
        var examples = new List<(TestFn, string)> { (FocusTest.Test, "Single focus test"),
            (BesselBeamTest.Test, "Bessel beam test"),
            (PlaneWaveTest.Test, "Plane wave test"),
            (WavTest.Test, "Wav modulation test"),
            (FociSTMTest.Test, "FociSTM test"),
            (GainSTMTest.Test, "GainSTM test"),
            (GainHoloTest.Test, "Multiple foci test"),
            (FlagTest.Test, "Flag test"),
            (CustomGain.Test, "Custom gain test"),
            (GroupByTransducerTest.Test, "Group (by Transducer) test")
        };
        if (autd.NumDevices() >= 2) examples.Add((GroupByDeviceTest.Test, "Group (by Device) test"));

        Console.WriteLine("======== AUTD3 firmware information ========");
        Console.WriteLine(string.Join("\n", autd.FirmwareVersion()));
        Console.WriteLine("============================================");

        while (true)
        {
            Console.WriteLine(string.Join("\n", examples.Select((example, i) => $"[{i}]: {example.Item2}")));
            Console.WriteLine("[Others]: finish");
            Console.Write("Choose number: ");

            if (!int.TryParse(Console.ReadLine(), out var idx) || idx >= examples.Count) break;

            var fn = examples[idx].Item1;
            fn(autd);

            Console.WriteLine("press any key to finish...");
            Console.ReadKey(true);

            Console.WriteLine("finish.");
            autd.Send((new Null(), new Silencer()));
        }

        autd.Close();
    }
}
