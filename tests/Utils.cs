namespace tests;

public static class Utils
{
    public static Controller<Audit> CreateController(int n = 2) => Controller.Open(Enumerable.Repeat(new AUTD3(), n), new Audit());
}
