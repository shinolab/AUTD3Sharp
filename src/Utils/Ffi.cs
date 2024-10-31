namespace AUTD3Sharp.Utils
{
    internal static class Ffi
    {
        internal static byte[] toNullTerminatedUtf8(string str)
        {
            var len = System.Text.Encoding.UTF8.GetByteCount(str);
            var bytes = new byte[len + 1];
            System.Text.Encoding.UTF8.GetBytes(str, 0, str.Length, bytes, bytes.GetLowerBound(0));
            return bytes;
        }
    }
}