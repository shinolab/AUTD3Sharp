using AUTD3Sharp;

namespace Samples;

internal static class FlagTest
{
    public static async Task Test<T>(Controller<T> autd)
    {
        Console.WriteLine("press any key to run fan...");
        Console.ReadKey(true);

        await autd.SendAsync(new ConfigureForceFan(_ => true), new ConfigureReadsFPGAState(_ => true));

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
            var states = await autd.FPGAStateAsync();
            Console.WriteLine($"{prompts[promptsIdx++ / 1000 % prompts.Length]} FPGA Status...");
            Console.WriteLine(string.Join("\n", states.Select((s, i) => s == null ? $"[{i}]: -" : $"[{i}]: {s.IsThermalAssert}")));
            Console.Write($"\x1b[{states.Length + 1}A");
        }

        th.Wait();

        await autd.SendAsync(new ConfigureForceFan(_ => false), new ConfigureReadsFPGAState(_ => false));
    }
}
