namespace tests;

public static class Utilities
{
    public static Controller<Audit> CreateController(int n = 2) => AUTD3Sharp.Controller.Open(Enumerable.Repeat(new AUTD3(), n), new Audit());
}
