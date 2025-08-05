using System;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Collections.Concurrent;
using System.Text;

namespace AssettoTelemetry.Networking
{
    //https://www.macoratti.net/18/03/c_listner1.htm
    public class WebSocketServer
    {
        //https://learn.microsoft.com/pt-br/dotnet/api/system.net.httplistener?view=net-9.0
        private readonly HttpListener _httpListener = new();
        //https://learn.microsoft.com/pt-br/dotnet/api/system.collections.concurrent.concurrentbag-1?view=net-8.0#methods
        private readonly ConcurrentBag<WebSocket> _clients = new();

        public WebSocketServer(string url = "http://localhost:8080/actelemetry/")
        {
            _httpListener.Prefixes.Add(url);
        }

        public async Task StartWebServerAsync()
        {
            _httpListener.Start();
            Console.WriteLine("Servidor web ouvindo em ws://localhost:8080/actelemetry/");

            while (true)
            {
                HttpListenerContext context = await _httpListener.GetContextAsync();

                if (context.Request.IsWebSocketRequest)
                {
                    var wsContext = await context.AcceptWebSocketAsync(null);
                    _clients.Add(wsContext.WebSocket);
                }
                else
                {
                    context.Response.StatusCode = 400; //BADREQUEST
                    context.Response.Close();
                }
            }
        }

        public async Task SendDataAsync(string data)
        {
            byte[] dataBuffer = Encoding.UTF8.GetBytes(data);
            foreach (var socket in _clients)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(dataBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }

    }
}