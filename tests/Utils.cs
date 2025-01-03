namespace tests;

public static class Utils
{
    public static async Task<Controller<Audit>> CreateController()
    {
        return await Controller.Builder([new AUTD3(Point3.Origin), new AUTD3(Point3.Origin)]).OpenAsync(Audit.Builder());
    }

    public static Controller<Audit> CreateControllerSync()
    {
        return Controller.Builder([new AUTD3(Point3.Origin), new AUTD3(Point3.Origin)]).Open(Audit.Builder());
    }
}
