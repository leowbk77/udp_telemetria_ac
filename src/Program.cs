using AssettoTelemetry.Networking;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new UdpTelemetryClient("192.168.100.31", 9996);
        await client.StartAsync();
        Console.WriteLine("Dados recebidos.");
    }
}