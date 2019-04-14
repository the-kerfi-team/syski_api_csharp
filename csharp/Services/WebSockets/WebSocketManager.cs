using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets
{
    public class WebSocketManager
    {

        private ConcurrentDictionary<Guid, WebSocketConnection> _WebSockets = new ConcurrentDictionary<Guid, WebSocketConnection>();
        private ConcurrentDictionary<Guid, Guid> _WebSocketSystem = new ConcurrentDictionary<Guid, Guid>();

        public WebSocketManager()
        {
        }

        public ConcurrentDictionary<Guid, WebSocketConnection> GetWebSockets()
        {
            return _WebSockets;
        }

        public WebSocketConnection GetSocketById(Guid id)
        {
            return _WebSockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public Guid GetId(WebSocketConnection webSocket)
        {
            return _WebSockets.FirstOrDefault(p => p.Value == webSocket).Key;
        }

        public Guid GetWebSocketIdFromSystemId(Guid systemId)
        {
            return _WebSocketSystem.FirstOrDefault(p => p.Value.Equals(systemId)).Key;
        }

        public async Task AddSocket(Guid id, WebSocketConnection webSocket)
        {
            _WebSockets.TryAdd(id, webSocket);
        }

        public async Task RemoveSocket(Guid id)
        {
            _WebSockets.TryRemove(id, out WebSocketConnection webSocket);
            _WebSocketSystem.TryRemove(id, out Guid systemId);
        }

        public async Task AddSystemToSocketLink(Guid webSocketId, Guid systemId)
        {
            _WebSocketSystem.TryAdd(webSocketId, systemId);
        }

    }
}
