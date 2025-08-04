using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

/*
    https://docs.google.com/document/d/1KfkZiIluXZ6mMhLWfDX1qAGbvhGRC3ZUzjVIt5FQpp4/pub
    Structs de datagrama da documentação estão erradas :)

    https://www.assettocorsa.net/forum/index.php?threads/udp-format.43612/
    https://docs.google.com/spreadsheets/d/1UTgeE7vbnGIzDz-URRk2eBIPc_LR1vWcZklp7xD9N0Y/edit?gid=488839664#gid=488839664
    https://github.com/bradland/ac_telemetry/tree/master

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
class Program
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    struct HandShaker
    {
        public int identifier;
        public int version;
        public int operationId;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct HandshakerResponse
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

    /*
        Struct CarInfo errada
    */
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

    static async Task Main(string[] args)
    {
        string endereco = "192.168.100.31";
        int port = 9995;
        int assetto_port = 9996;

        HandShaker handshake = new HandShaker
        {
            identifier = 1,
            version = 1,
            operationId = 0
        };
        int handshake_size = Marshal.SizeOf(handshake);
        byte[] buffer = new byte[handshake_size];

        using UdpClient udpClient = new UdpClient(port);
        Console.WriteLine($"[Assetto UDP] Escutando na porta {port}...");
        Console.WriteLine($"Tentando se conectar ao Assetto Corsa na porta {assetto_port}, do host {endereco}");
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(endereco), assetto_port);

        try
        {
            // primeiro envio (handshake)
            udpClient.Connect(ipEndPoint);
            Console.WriteLine($"connectado: {endereco}:{assetto_port}, enviando dados...");

            IntPtr ptr = Marshal.AllocHGlobal(handshake_size);
            Marshal.StructureToPtr(handshake, ptr, true);
            Marshal.Copy(ptr, buffer, 0, handshake_size);
            Marshal.FreeHGlobal(ptr);

            udpClient.Send(buffer, buffer.Length);
            var result = await udpClient.ReceiveAsync();
            var data = result.Buffer;
            // Recebe resposta do Assetto (handshake de volta)
            // Interpreta como resposta do handshake
            /*
                Não está lendo corretamente ainda pois a struct da documentação está errada
            */
            HandshakerResponse response = ByteArrayToStructure<HandshakerResponse>(data);
            Console.WriteLine($"Handshake OK: {ByteArrayToString(response.driverName)} com {ByteArrayToString(response.carName)} na pista {ByteArrayToString(response.trackName)} ({ByteArrayToString(response.trackConfig)})");

            // segundo envio (subscribe)
            Console.WriteLine("Enviando subscribe.");
            HandShaker handshake_dois = new HandShaker
            {
                identifier = 1,
                version = 1,
                operationId = 1
            };
            int handshake_size_dois = Marshal.SizeOf(handshake_dois);
            byte[] buffer_dois = new byte[handshake_size_dois];

            IntPtr ptr_dois = Marshal.AllocHGlobal(handshake_size_dois);
            Marshal.StructureToPtr(handshake_dois, ptr_dois, true);
            Marshal.Copy(ptr_dois, buffer_dois, 0, handshake_size_dois);
            Marshal.FreeHGlobal(ptr_dois);

            udpClient.Send(buffer_dois, buffer_dois.Length);
            Console.WriteLine("Recebendo telemetria...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }

        while (true)
        {
            try
            {
                var result = await udpClient.ReceiveAsync();
                var data = result.Buffer;

                float rpm = BitConverter.ToSingle(data.Skip(68).Take(4).ToArray(), 0);
                Console.WriteLine($"RPM: {rpm}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }

    static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
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

    static string ByteArrayToString(byte[] bytes)
    {
        int length = Array.IndexOf<byte>(bytes, 0); // posição do primeiro \0
        if (length < 0) length = bytes.Length;      // se não tiver \0
        return System.Text.Encoding.Unicode.GetString(bytes, 0, length);
    }
}

/*
*/