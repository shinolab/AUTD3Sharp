using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    public sealed class Fir : Driver.Datagram.Modulation
    {
        public Driver.Datagram.Modulation Target;
        public float[] Coef;

        public Fir(Driver.Datagram.Modulation target, IEnumerable<float> coef)
        {
            Target = target;
            Coef = coef as float[] ?? coef.ToArray();
        }

        internal override ModulationPtr ModulationPtr()
        {
            unsafe
            {
                fixed (float* pCoef = &Coef[0])
                    return NativeMethodsBase.AUTDModulationWithFir(Target.ModulationPtr(), pCoef, (uint)Coef.Length);
            }
        }
    }
}
