using System;

namespace AUTD3Sharp
{
    [Serializable]
    public class AUTDException : Exception
    {
        public AUTDException(byte[] value) : base(System.Text.Encoding.UTF8.GetString(value).TrimEnd('\0')) { }
        public AUTDException(string msg) : base(msg) { }

        public static AUTDException InvalidFreqType() => new("Freq type must be float or uint.");
    }
}
