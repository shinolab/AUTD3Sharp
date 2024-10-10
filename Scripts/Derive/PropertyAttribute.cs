using System;

namespace AUTD3Sharp.Derive
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class BuilderAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyAttribute : Attribute
    {
        public bool EmitIntensity { get; set; } = false;
        public bool Phase { get; set; } = false;
    }
}
