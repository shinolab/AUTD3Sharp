namespace AUTD3Sharp.Derive;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ModulationAttribute : Attribute
{
    public bool NoCache { get; set; } = false;
    public bool NoRadiationPressure { get; set; } = false;
    public bool NoTransform { get; set; } = false;
    public bool ConfigNoChange { get; set; } = false;
}
