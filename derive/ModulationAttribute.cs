using System;

namespace AUTD3Sharp.Derive
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ModulationAttribute : Attribute
    {
        public bool ConfigNoChange { get; set; } = false;
    }
}
