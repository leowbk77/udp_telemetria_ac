using System.Runtime.InteropServices;

namespace AssettoTelemetry.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct HandShaker
    {
        public int identifier;
        public int version;
        public int operationId;
    }
}
