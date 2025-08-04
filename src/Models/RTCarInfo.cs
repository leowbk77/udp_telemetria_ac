using System.Runtime.InteropServices;

/* 
Struct CarInfo errada
Dados reais
 \/\/
=======Endereços dos dos dados da struct RTCarInfo===================
    SpeedKMH = BitConverter.ToSingle(UDPBytes.Skip(8).Take(4).ToArray, 0)
    SpeedMPH = BitConverter.ToSingle(UDPBytes.Skip(12).Take(4).ToArray, 0)
    SpeedMS = BitConverter.ToSingle(UDPBytes.Skip(16).Take(4).ToArray, 0)
    RPM = BitConverter.ToSingle(UDPBytes.Skip(68).Take(4).ToArray, 0)
    Gear = BitConverter.ToInt32(UDPBytes.Skip(76).Take(4).ToArray, 0) - 1
    LapTime = BitConverter.ToUInt32(UDPBytes.Skip(40).Take(4).ToArray, 0)
    LastLap = BitConverter.ToUInt32(UDPBytes.Skip(44).Take(4).ToArray, 0)
    BestLap = BitConverter.ToUInt32(UDPBytes.Skip(48).Take(4).ToArray, 0)
    LapCount = BitConverter.ToUInt32(UDPBytes.Skip(52).Take(4).ToArray, 0)
    GForceVert = BitConverter.ToSingle(UDPBytes.Skip(28).Take(4).ToArray, 0)
    GForceLon = BitConverter.ToSingle(UDPBytes.Skip(36).Take(4).ToArray, 0)
    GForceLat = BitConverter.ToSingle(UDPBytes.Skip(32).Take(4).ToArray, 0)
    Gas = BitConverter.ToSingle(UDPBytes.Skip(56).Take(4).ToArray, 0)
    Brake = BitConverter.ToSingle(UDPBytes.Skip(60).Take(4).ToArray, 0)
    Clutch = BitConverter.ToSingle(UDPBytes.Skip(64).Take(4).ToArray, 0)
    Steer = BitConverter.ToSingle(UDPBytes.Skip(72).Take(4).ToArray, 0)
    PositionNormalized = BitConverter.ToSingle(UDPBytes.Skip(308).Take(4).ToArray, 0)
    CarPosition(0) = BitConverter.ToSingle(UDPBytes.Skip(316).Take(4).ToArray, 0)
    CarPosition(1) = BitConverter.ToSingle(UDPBytes.Skip(320).Take(4).ToArray, 0)
    CarPosition(2) = BitConverter.ToSingle(UDPBytes.Skip(324).Take(4).ToArray, 0)
    CarSLope = BitConverter.ToSingle(UDPBytes.Skip(312).Take(4).ToArray, 0)
*/

namespace AssettoTelemetry.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct RTCarInfo
    {
        public byte identifier;
        public int size;

        public float speed_Kmh;
        public float speed_Mph;
        public float speed_Ms;

        [MarshalAs(UnmanagedType.U1)] public bool isAbsEnabled;
        [MarshalAs(UnmanagedType.U1)] public bool isAbsInAction;
        [MarshalAs(UnmanagedType.U1)] public bool isTcInAction;
        [MarshalAs(UnmanagedType.U1)] public bool isTcEnabled;
        [MarshalAs(UnmanagedType.U1)] public bool isInPit;
        [MarshalAs(UnmanagedType.U1)] public bool isEngineLimiterOn;

        public float accG_vertical;
        public float accG_horizontal;
        public float accG_frontal;

        public int lapTime;
        public int lastLap;
        public int bestLap;
        public int lapCount;

        public float gas;
        public float brake;
        public float clutch;
        public float engineRPM;
        public float steer;
        public int gear;
        public float cgHeight;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] wheelAngularSpeed;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] slipAngle;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] slipAngle_ContactPatch;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] slipRatio;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] tyreSlip;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] ndSlip;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] load;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] Dy;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] Mz;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] tyreDirtyLevel;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] camberRAD;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] tyreRadius;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] tyreLoadedRadius;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] suspensionHeight;

        public float carPositionNormalized;
        public float carSlope;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] carCoordinates;
    }

}