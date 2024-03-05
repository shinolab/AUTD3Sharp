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
        public bool IsThermalAssert => (_info & (1 << 0)) != 0;

        public Segment CurrentModSegment => (_info & (1 << 1)) switch
        {
            0 => Segment.S0,
            _ => Segment.S1
        };

        public Segment? CurrentGainSegment
        {
            get
            {
                {
                    if (!IsGainMode) return null;
                    return (_info & (1 << 2)) switch
                    {
                        0 => Segment.S0,
                        _ => Segment.S1
                    };
                }
            }
        }

        public Segment? CurrentSTMSegment
        {
            get
            {
                {
                    if (!IsSTMMode) return null;
                    return (_info & (1 << 2)) switch
                    {
                        0 => Segment.S0,
                        _ => Segment.S1
                    };
                }
            }
        }

        public bool IsGainMode => (_info & (1 << 3)) != 0;
        public bool IsSTMMode => !IsGainMode;

        public override string ToString()
        {
            return $"Thermal assert = {IsThermalAssert}";
        }
    }
}
