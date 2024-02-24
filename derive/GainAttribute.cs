namespace AUTD3Sharp.Derive;

[AttributeUsage(AttributeTargets.Class)]
public sealed class GainAttribute : Attribute
{
    public bool NoCache { get; set; } = false;
    public bool NoTransform { get; set; } = false;
}
