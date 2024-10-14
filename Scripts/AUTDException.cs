using System;

namespace AUTD3Sharp
{
    [Serializable]
    public class AUTDException : Exception
    {
        public AUTDException(byte[] value)
            : base($"AUTDException: {System.Text.Encoding.UTF8.GetString(value).TrimEnd('\0')}")
        {
        }

        public AUTDException(string msg)
            : base($"AUTDException: {msg}")
        {
        }
    }
}
