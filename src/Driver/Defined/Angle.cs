using System;
using System.Diagnostics.CodeAnalysis;

#if UNITY_2020_2_OR_NEWER
#nullable enable
#endif

namespace AUTD3Sharp
{
    public readonly struct Angle
    {
        public static Angle FromDegree(float degree) => new(degree / 180 * MathF.PI);
        public static Angle FromRadian(float radian) => new(radian);

        public float Radian { get; }

        public class UnitRadian
        {
            internal UnitRadian() { }
            public static Angle operator *(float a, UnitRadian _) => FromRadian(a);
        }
        public class UnitDegree
        {
            internal UnitDegree() { }
            public static Angle operator *(float a, UnitDegree _) => FromDegree(a);
        }

        private Angle(float value) => Radian = value;

        public static bool operator ==(Angle left, Angle right) => left.Equals(right);
        public static bool operator !=(Angle left, Angle right) => !left.Equals(right);
        public bool Equals(Angle other) => Radian.Equals(other.Radian);
        public override bool Equals(object? obj) => obj is Angle other && Equals(other);
        [ExcludeFromCodeCoverage] public override int GetHashCode() => Radian.GetHashCode();

        internal NativeMethods.Angle ToNative() => new() { radian = Radian };
    }

    public static partial class Units
    {
#pragma warning disable IDE1006
        public static Angle.UnitRadian rad { get; } = new();
        public static Angle.UnitDegree deg { get; } = new();
#pragma warning restore IDE1006
    }
}

#if UNITY_2020_2_OR_NEWER
#nullable restore
#endif
