using System.Text;
using AUTD3Sharp.NativeMethods;

namespace AUTD3Sharp.Driver
{
    public readonly struct FirmwareInfo
    {
        public string Info { get; }

        public static string LatestVersion
        {
            get
            {
                var latest = new byte[256];
                unsafe
                {
                    fixed (byte* l = &latest[0])
                        NativeMethodsBase.AUTDFirmwareLatest(l);
                }
                return Encoding.UTF8.GetString(latest).TrimEnd('\0');
            }
        }

        internal FirmwareInfo(string info)
        {
            Info = info.TrimEnd('\0');
        }

        public override string ToString() => Info;
    }
}
