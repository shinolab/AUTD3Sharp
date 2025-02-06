using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Driver.Datagram;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Modulation
{
    public sealed class Fir : IModulation
    {
        public IModulation Target;
        public float[] Coef;

        public Fir(IModulation target, IEnumerable<float> coef)
        {
            Target = target;
            Coef = coef as float[] ?? coef.ToArray();
        }

        ModulationPtr IModulation.ModulationPtr()
        {
            unsafe
            {
                fixed (float* pCoef = &Coef[0])
                    return NativeMethodsBase.AUTDModulationWithFir(Target.ModulationPtr(), pCoef, (uint)Coef.Length);
            }
        }
    }
}
