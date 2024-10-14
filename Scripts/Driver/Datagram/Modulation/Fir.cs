using System.Collections.Generic;
using System.Linq;
using AUTD3Sharp.Derive;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver.Datagram.Modulation
{
    [Modulation(ConfigNoChange = true)]
    public sealed partial class Fir<TM>
    where TM : IModulation
    {
        private readonly TM _m;
        private readonly float[] _coef;

        public Fir(TM m, IEnumerable<float> iter)
        {
            _m = m;
            _coef = iter as float[] ?? iter.ToArray();
        }

        private ModulationPtr ModulationPtr()
        {
            unsafe
            {
                fixed (float* pCoef = &_coef[0])
                    return NativeMethodsBase.AUTDModulationWithFir(_m.ModulationPtr(), LoopBehavior, pCoef, (uint)_coef.Length);
            }
        }
    }
}
