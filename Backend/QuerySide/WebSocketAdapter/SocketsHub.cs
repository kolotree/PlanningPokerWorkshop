using System.Collections.Generic;
using WebSocketSharp;

namespace WebSocketAdapter
{
    internal class SocketsHub
    {
        private readonly Dictionary<string, List<WebSocket>> _socketsBySessionId;

        public SocketsHub()
        {
            _socketsBySessionId = new Dictionary<string, List<WebSocket>>();
        }

        public void Add(string sessionId, WebSocket socket)
        {
            if (_socketsBySessionId.TryGetValue(sessionId, out var sockets))
            {
                sockets.Add(socket);
            }
            else
            {
                _socketsBySessionId.Add(sessionId, new List<WebSocket> {socket});
            }
        }
        
        public void Remove(string sessionId, WebSocket socket)
        {
            if (_socketsBySessionId.TryGetValue(sessionId, out var sockets))
            {
                sockets.Remove(socket);
            }
        }

        public void NotifyAll(string sessionId, string message)
        {
            if (!_socketsBySessionId.TryGetValue(sessionId, out var sockets)) return;
            foreach (var socket in sockets)
            {
                socket.Send(message);
            }
        }
    }
}