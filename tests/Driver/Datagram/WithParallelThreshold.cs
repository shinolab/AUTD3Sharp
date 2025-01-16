namespace tests;

public class WithParallelThresholdTest
{
    [Fact]
    public void TestWithParallelThreshold()
    {
        var autd = Controller.Builder([new AUTD3(Point3.Origin)])
            .Open(Audit.Builder());

        autd.Send(new Null().WithParallelThreshold(null));
        Assert.Null(autd.Link.LastParallelThreshold());

        autd.Send(new Null().WithParallelThreshold(10));
        Assert.Equal(10, autd.Link.LastParallelThreshold());

        autd.Send((new Static(), new Null()).WithParallelThreshold(20));
        Assert.Equal(20, autd.Link.LastParallelThreshold());
    }
}
