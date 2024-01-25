namespace AUTD3Sharp.Driver.FPGA.Defined
{
    public class FPGAState
    {
        private readonly byte _info;

        internal FPGAState(byte info)
        {
            _info = info;
        }

        /// <summary>
        /// Check if thermal sensor is asserted
        /// </summary>
        public bool IsThermalAssert => (_info & 0x01) != 0;

        public override string ToString()
        {
            return $"Thermal assert = {IsThermalAssert}";
        }
    }
}
