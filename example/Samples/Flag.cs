using AUTD3Sharp;

namespace Samples;

internal static class FlagTest
{
    public static void Test(Controller autd)
    {
        Console.WriteLine("press any key to run fan...");
        Console.ReadKey(true);

        autd.Send((new ForceFan(_ => true), new ReadsFPGAState(_ => true)));

        var fin = false;
        Console.WriteLine("press any key stop checking FPGA status...");
        var th = Task.Run(() =>
        {
            Console.ReadKey(true);
            fin = true;
        });

        var prompts = new[] { '-', '/', '|', '\\' };
        var promptsIdx = 0;
        while (!fin)
        {
            var states = autd.FPGAState();
            Console.WriteLine($"{prompts[promptsIdx++ / 1000 % prompts.Length]} FPGA Status...");
            Console.WriteLine(string.Join("\n", states.Select((s, i) => s is null ? $"[{i}]: -" : $"[{i}]: {s.IsThermalAssert}")));
            Console.Write($"\x1b[{states.Length + 1}A");
        }

        th.Wait();

        autd.Send((new ForceFan(_ => false), new ReadsFPGAState(_ => false)));
    }
}
