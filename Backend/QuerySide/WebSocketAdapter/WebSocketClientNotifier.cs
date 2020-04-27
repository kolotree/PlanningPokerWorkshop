using System;
using System.Collections.Specialized;
using Ports;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketAdapter
{
    public sealed class WebSocketClientNotifier: IClientNotifier, IDisposable
    {
        private WebSocketServer _webSocketServer;
        private readonly SocketsHub _socketsHub;

        public WebSocketClientNotifier()
        {
            _socketsHub = new SocketsHub();
        }

        public void StartClientNotifier()
        {
            _webSocketServer = InitializeWebSocketServer();
            _webSocketServer.Start();
        }

        public void NotifyAllClients(string sessionId, string message) => 
            _socketsHub.NotifyAll(sessionId, message);

        private WebSocketServer InitializeWebSocketServer()
        {
            var server = new WebSocketServer(4649, false);
            server.AddWebSocketService("/ClientHub", () => new Connection(OnConnectionOpened, OnConnectionClosed));
            return server;
        }

        private void OnConnectionOpened(WebSocket socket, NameValueCollection queryString)
        {
            var sessionId = queryString["sessionId"];
            if (sessionId != null)
            {
                _socketsHub.Add(sessionId, socket);
            }
        }

        private void OnConnectionClosed(WebSocket socket, NameValueCollection queryString)
        {
            var sessionId = queryString["sessionId"];
            if (sessionId != null)
            {
                _socketsHub.Remove(sessionId, socket);
            }
        }
        
        public void Dispose() => 
            _webSocketServer?.Stop();
    }
}