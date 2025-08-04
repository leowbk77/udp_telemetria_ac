using System.Runtime.InteropServices;

namespace AssettoTelemetry.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HandshakerResponse
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public byte[] carName;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public byte[] driverName;

        public int identifier;
        public int version;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public byte[] trackName;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public byte[] trackConfig;
    }
}
