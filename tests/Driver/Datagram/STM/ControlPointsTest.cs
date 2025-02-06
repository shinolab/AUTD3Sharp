namespace tests.Driver.Datagram.STM;

public class ControlPointsTest
{
    [Fact]
    public void ControlPoint()
    {
        {
            var c = new ControlPoint
            {
                Point = new Point3(1, 2, 3),
                PhaseOffset = new Phase(0xFF),
            };
            Assert.Equal(new Point3(1, 2, 3), c.Point);
            Assert.Equal(new Phase(0xFF), c.PhaseOffset);
        }
        {
            var c = new ControlPoint();
            Assert.Equal(new Point3(0, 0, 0), c.Point);
            Assert.Equal(new Phase(0x00), c.PhaseOffset);
        }
    }

    [Fact]
    public void ControlPoints1()
    {
        {
            var c = new ControlPoints1
            {
                Points = [new ControlPoint(new Point3(1, 2, 3))],
                Intensity = EmitIntensity.Min,
            };
            Assert.Equal([new ControlPoint(new Point3(1, 2, 3))], c.Points);
            Assert.Equal(EmitIntensity.Min, c.Intensity);
        }
        {
            var c = new ControlPoints1(new Point3(1, 2, 3));
            Assert.Equal([new ControlPoint(new Point3(1, 2, 3))], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
        {
            var c = new ControlPoints1();
            Assert.Equal([new ControlPoint()], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
    }

    [Fact]
    public void ControlPoints2()
    {
        {
            var c = new ControlPoints2
            {
                Points = [new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6))],
                Intensity = EmitIntensity.Min,
            };
            Assert.Equal([new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6))], c.Points);
            Assert.Equal(EmitIntensity.Min, c.Intensity);
        }
        {
            var c = new ControlPoints2((new Point3(1, 2, 3), new Point3(4, 5, 6)));
            Assert.Equal([new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6))], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
        {
            var c = new ControlPoints2();
            Assert.Equal([new ControlPoint(), new ControlPoint()], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
    }

    [Fact]
    public void ControlPoints3()
    {
        {
            var c = new ControlPoints3
            {
                Points =
                [
                    new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                    new ControlPoint(new Point3(7, 8, 9))
                ],
                Intensity = EmitIntensity.Min,
            };
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9))
            ], c.Points);
            Assert.Equal(EmitIntensity.Min, c.Intensity);
        }
        {
            var c = new ControlPoints3((new Point3(1, 2, 3), new Point3(4, 5, 6), new Point3(7, 8, 9)));
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9))
            ], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
        {
            var c = new ControlPoints3();
            Assert.Equal([new ControlPoint(), new ControlPoint(), new ControlPoint()], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
    }

    [Fact]
    public void ControlPoints4()
    {
        {
            var c = new ControlPoints4
            {
                Points =
                [
                    new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                    new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12))
                ],
                Intensity = EmitIntensity.Min,
            };
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12))
            ], c.Points);
            Assert.Equal(EmitIntensity.Min, c.Intensity);
        }
        {
            var c = new ControlPoints4((new Point3(1, 2, 3), new Point3(4, 5, 6), new Point3(7, 8, 9),
                new Point3(10, 11, 12)));
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12))
            ], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
        {
            var c = new ControlPoints4();
            Assert.Equal([new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint()], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
    }

    [Fact]
    public void ControlPoints5()
    {
        {
            var c = new ControlPoints5
            {
                Points =
                [
                    new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                    new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                    new ControlPoint(new Point3(13, 14, 15))
                ],
                Intensity = EmitIntensity.Min,
            };
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                new ControlPoint(new Point3(13, 14, 15))
            ], c.Points);
            Assert.Equal(EmitIntensity.Min, c.Intensity);
        }
        {
            var c = new ControlPoints5((new Point3(1, 2, 3), new Point3(4, 5, 6), new Point3(7, 8, 9),
                new Point3(10, 11, 12), new Point3(13, 14, 15)));
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                new ControlPoint(new Point3(13, 14, 15))
            ], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
        {
            var c = new ControlPoints5();
            Assert.Equal(
                [new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint()],
                c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
    }

    [Fact]
    public void ControlPoints6()
    {
        {
            var c = new ControlPoints6
            {
                Points =
                [
                    new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                    new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                    new ControlPoint(new Point3(13, 14, 15)), new ControlPoint(new Point3(16, 17, 18))
                ],
                Intensity = EmitIntensity.Min,
            };
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                new ControlPoint(new Point3(13, 14, 15)), new ControlPoint(new Point3(16, 17, 18))
            ], c.Points);
            Assert.Equal(EmitIntensity.Min, c.Intensity);
        }
        {
            var c = new ControlPoints6((new Point3(1, 2, 3), new Point3(4, 5, 6), new Point3(7, 8, 9),
                new Point3(10, 11, 12), new Point3(13, 14, 15), new Point3(16, 17, 18)));
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                new ControlPoint(new Point3(13, 14, 15)), new ControlPoint(new Point3(16, 17, 18))
            ], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
        {
            var c = new ControlPoints6();
            Assert.Equal(
            [
                new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(),
                new ControlPoint()
            ], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
    }

    [Fact]
    public void ControlPoints7()
    {
        {
            var c = new ControlPoints7
            {
                Points =
                [
                    new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                    new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                    new ControlPoint(new Point3(13, 14, 15)), new ControlPoint(new Point3(16, 17, 18)),
                    new ControlPoint(new Point3(19, 20, 21))
                ],
                Intensity = EmitIntensity.Min,
            };
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                new ControlPoint(new Point3(13, 14, 15)), new ControlPoint(new Point3(16, 17, 18)),
                new ControlPoint(new Point3(19, 20, 21))
            ], c.Points);
            Assert.Equal(EmitIntensity.Min, c.Intensity);
        }
        {
            var c = new ControlPoints7((new Point3(1, 2, 3), new Point3(4, 5, 6), new Point3(7, 8, 9),
                new Point3(10, 11, 12), new Point3(13, 14, 15), new Point3(16, 17, 18), new Point3(19, 20, 21)));
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                new ControlPoint(new Point3(13, 14, 15)), new ControlPoint(new Point3(16, 17, 18)),
                new ControlPoint(new Point3(19, 20, 21))
            ], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
        {
            var c = new ControlPoints7();
            Assert.Equal(
            [
                new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(),
                new ControlPoint(), new ControlPoint()
            ], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
    }

    [Fact]
    public void ControlPoints8()
    {
        {
            var c = new ControlPoints8
            {
                Points =
                [
                    new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                    new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                    new ControlPoint(new Point3(13, 14, 15)), new ControlPoint(new Point3(16, 17, 18)),
                    new ControlPoint(new Point3(19, 20, 21)), new ControlPoint(new Point3(22, 23, 24))
                ],
                Intensity = EmitIntensity.Min,
            };
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                new ControlPoint(new Point3(13, 14, 15)), new ControlPoint(new Point3(16, 17, 18)),
                new ControlPoint(new Point3(19, 20, 21)), new ControlPoint(new Point3(22, 23, 24))
            ], c.Points);
            Assert.Equal(EmitIntensity.Min, c.Intensity);
        }
        {
            var c = new ControlPoints8((new Point3(1, 2, 3), new Point3(4, 5, 6), new Point3(7, 8, 9),
                new Point3(10, 11, 12), new Point3(13, 14, 15), new Point3(16, 17, 18), new Point3(19, 20, 21),
                new Point3(22, 23, 24)));
            Assert.Equal(
            [
                new ControlPoint(new Point3(1, 2, 3)), new ControlPoint(new Point3(4, 5, 6)),
                new ControlPoint(new Point3(7, 8, 9)), new ControlPoint(new Point3(10, 11, 12)),
                new ControlPoint(new Point3(13, 14, 15)), new ControlPoint(new Point3(16, 17, 18)),
                new ControlPoint(new Point3(19, 20, 21)), new ControlPoint(new Point3(22, 23, 24))
            ], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
        {
            var c = new ControlPoints8();
            Assert.Equal(
            [
                new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(), new ControlPoint(),
                new ControlPoint(), new ControlPoint(), new ControlPoint()
            ], c.Points);
            Assert.Equal(EmitIntensity.Max, c.Intensity);
        }
    }
}
