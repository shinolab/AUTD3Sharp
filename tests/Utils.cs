namespace tests;

public static class Utils
{
    public static Controller<Audit> CreateController()
    {
        return Controller.Builder([new AUTD3(Point3.Origin), new AUTD3(Point3.Origin)]).Open(Audit.Builder());
    }
}
