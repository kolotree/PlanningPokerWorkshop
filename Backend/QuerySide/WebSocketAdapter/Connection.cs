using System;
using System.Collections.Specialized;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketAdapter
{
    public sealed class Connection : WebSocketBehavior
    {
        private readonly Action<WebSocket, NameValueCollection> _onConnectionOpened;
        private readonly Action<WebSocket, NameValueCollection> _onConnectionClosed;

        public Connection(Action<WebSocket, NameValueCollection> onConnectionOpened, Action<WebSocket, NameValueCollection> onConnectionClosed)
        {
            _onConnectionOpened = onConnectionOpened;
            _onConnectionClosed = onConnectionClosed;
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            _onConnectionOpened(Context.WebSocket, Context.QueryString);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            _onConnectionClosed(Context.WebSocket, Context.QueryString);
        }
    }
}