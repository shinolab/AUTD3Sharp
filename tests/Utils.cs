namespace tests;

public static class Utils
{
    public static async Task<Controller<Audit>> CreateController()
    {
        return await new ControllerBuilder([new AUTD3(Vector3.Zero), new AUTD3(Vector3.Zero)]).OpenAsync(Audit.Builder());
    }

    public static Controller<Audit> CreateControllerSync()
    {
        return new ControllerBuilder([new AUTD3(Vector3.Zero), new AUTD3(Vector3.Zero)]).Open(Audit.Builder());
    }
}
