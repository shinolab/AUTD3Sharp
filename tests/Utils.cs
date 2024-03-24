namespace tests;

public static class Utils
{
    public static async Task<Controller<Audit>> CreateController()
    {
        return await new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).AddDevice(new AUTD3(Vector3d.Zero)).OpenAsync(Audit.Builder());
    }

    public static Controller<Audit> CreateControllerSync()
    {
        return new ControllerBuilder().AddDevice(new AUTD3(Vector3d.Zero)).AddDevice(new AUTD3(Vector3d.Zero)).Open(Audit.Builder());
    }
}
