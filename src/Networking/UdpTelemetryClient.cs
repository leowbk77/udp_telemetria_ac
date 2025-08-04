using AssettoTelemetry.Models;
using AssettoTelemetry.Utils;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AssettoTelemetry.Networking
{
    public class UdpTelemetryClient
    {
        private readonly string _host;
        private readonly int _listenPort;
        private readonly int _assettoPort;
        private UdpClient _udpClient;

        public UdpTelemetryClient(string host, int assettoPort)
        {
            _host = host;
            _listenPort = 9995;
            _assettoPort = assettoPort;
        }

        public async Task StartAsync()
        {
            _udpClient = new UdpClient(_listenPort);
            var endPoint = new IPEndPoint(IPAddress.Parse(_host), _assettoPort);

            Console.WriteLine($"[Assetto UDP] Escutando na porta {_listenPort}...");
            Console.WriteLine($"Tentando se conectar ao Assetto em: {_host}:{_assettoPort}");

            try
            {
                _udpClient.Connect(endPoint);

                await SendHandshake(0);
                var response = await _udpClient.ReceiveAsync();
                var handshakeResponse = ByteUtils.ByteArrayToStructure<HandshakerResponse>(response.Buffer);

                Console.WriteLine($"Conectado: {ByteUtils.ByteArrayToString(handshakeResponse.driverName)} com {ByteUtils.ByteArrayToString(handshakeResponse.carName)} na pista {ByteUtils.ByteArrayToString(handshakeResponse.trackName)} ({ByteUtils.ByteArrayToString(handshakeResponse.trackConfig)})");

                await SendHandshake(1);
                Console.WriteLine("Recebendo telemetria...");

                // _ = Task.Run(() => StartReceivingLoopAsync()); // roda fora da thread principal
                while (true)
                {
                    var data = (await _udpClient.ReceiveAsync()).Buffer;
                    float rpm = BitConverter.ToSingle(data.Skip(68).Take(4).ToArray(), 0);
                    Console.WriteLine($"RPM: {rpm}");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private async Task SendHandshake(int operationId)
        {
            var handshake = new HandShaker { identifier = 1, version = 1, operationId = operationId };
            int size = Marshal.SizeOf(handshake);
            byte[] buffer = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(handshake, ptr, true);
            Marshal.Copy(ptr, buffer, 0, size);
            Marshal.FreeHGlobal(ptr);

            await _udpClient.SendAsync(buffer, buffer.Length);
        }

        private async Task StartReceivingLoopAsync()
        {
            while(true)
            {
                try
                {
                    var data = (await _udpClient.ReceiveAsync()).Buffer;
                    float rpm = BitConverter.ToSingle(data.Skip(68).Take(4).ToArray(), 0);
                    Console.WriteLine($"RPM: {rpm}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro recebendo telemetria: {ex.Message}");
                }
            }
        }

    }
}
