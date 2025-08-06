using AssettoTelemetry.Networking;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        WebSocketServer webSocketServer = new WebSocketServer();
        _ = Task.Run(() => webSocketServer.StartWebServerAsync());
        var client = new UdpTelemetryClient("192.168.100.31", 9996, webSocketServer);
        await client.StartAsync();
        Console.WriteLine("Dados recebidos.");
    }
}