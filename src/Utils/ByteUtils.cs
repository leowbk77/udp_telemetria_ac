using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AssettoTelemetry.Utils
{
    public static class ByteUtils
    {
        public static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            }
            finally
            {
                handle.Free();
            }
        }

        public static string ByteArrayToString(byte[] bytes)
        {
            int length = Array.IndexOf(bytes, (byte)0);
            if (length < 0) length = bytes.Length;
            return Encoding.Unicode.GetString(bytes, 0, length);
        }
    }
}
