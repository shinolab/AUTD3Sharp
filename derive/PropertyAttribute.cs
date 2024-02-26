using System;

namespace AUTD3Sharp.Derive
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class BuilderAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyAttribute : Attribute
    {
        public bool EmitIntensity { get; set; } = false;
    }
}
