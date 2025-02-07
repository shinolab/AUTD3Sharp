namespace tests.Utils;

public class QuaternionTests
{
    [Fact]
    public void Constructor_WithThreeArguments_SetsPropertiesCorrectly()
    {
        var q = new Quaternion(1, 2, 3, 4);

        Assert.Equal(1, q.W);
        Assert.Equal(2, q.X);
        Assert.Equal(3, q.Y);
        Assert.Equal(4, q.Z);
    }

    [Fact]
    public void Construct_Identity()
    {
        var q = Quaternion.Identity;

        Assert.Equal(1, q.W);
        Assert.Equal(0, q.X);
        Assert.Equal(0, q.Y);
        Assert.Equal(0, q.Z);
    }

    [Fact]
    public void GetHashCode_ReturnsConsistentHashCodes()
    {
        var q1 = new Quaternion(1, 2, 3, 4);
        var q2 = new Quaternion(1, 2, 3, 4);

        Assert.Equal(q1.GetHashCode(), q2.GetHashCode());
    }

    [Fact]
    public void Normalized_ReturnsNormalizedVector()
    {
        var q = new Quaternion(1, 2, 2, 4);

        var result = q.Normalized;

        Assert.Equal(1 / 5.0f, result.W);
        Assert.Equal(2 / 5.0f, result.X);
        Assert.Equal(2 / 5.0f, result.Y);
        Assert.Equal(4 / 5.0f, result.Z);
    }

    [Fact]
    public void L2Norm_ReturnsCorrectNorm()
    {
        var q = new Quaternion(1, 2, 2, 4);

        var result = q.L2Norm;

        Assert.Equal(5, result);
    }

    [Fact]
    public void L2NormSquared_ReturnsCorrectNormSquared()
    {
        var q = new Quaternion(1, 2, 2, 4);

        var result = q.L2NormSquared;

        Assert.Equal(25, result);
    }

    [Fact]
    public void Equals_ReturnsTrueForEqualQuaternion()
    {
        var q1 = new Quaternion(1, 2, 3, 4);
        var q2 = new Quaternion(1, 2, 3, 4);
        var q3 = new Quaternion(2, 2, 3, 4);
        var q4 = new Quaternion(1, 3, 3, 4);
        var q5 = new Quaternion(1, 2, 4, 4);
        var q6 = new Quaternion(1, 2, 3, 5);

        Assert.True(q1 == q2);
        Assert.True(q1 != q3);
        Assert.True(q1 != q4);
        Assert.True(q1 != q5);
        Assert.True(q1 != q6);
        Assert.True(q1.Equals((object)q2));
        Assert.True(!q1.Equals((object)q3));
        Assert.True(!q1.Equals(null));
    }

    [Fact]
    public void ToString_ReturnsCorrectString()
    {
        var q = new Quaternion(1, 2, 3, 4);

        var result = q.ToString();

        Assert.Equal("(1, 2, 3, 4)", result);
    }
}