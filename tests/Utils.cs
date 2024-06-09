namespace tests;

public static class Utils
{
    public static async Task<Controller<Audit>> CreateController()
    {
        return await new ControllerBuilder().AddDevice(new AUTD3(Vector3.Zero)).AddDevice(new AUTD3(Vector3.Zero)).OpenAsync(Audit.Builder());
    }

    public static Controller<Audit> CreateControllerSync()
    {
        return new ControllerBuilder().AddDevice(new AUTD3(Vector3.Zero)).AddDevice(new AUTD3(Vector3.Zero)).Open(Audit.Builder());
    }
}
