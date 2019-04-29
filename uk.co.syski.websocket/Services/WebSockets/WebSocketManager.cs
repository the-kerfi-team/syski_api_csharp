using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Syski.WebSocket.Services.WebSockets
{
    public class WebSocketManager
    {

        private ConcurrentDictionary<Guid, WebSocketConnection> webSockets = new ConcurrentDictionary<Guid, WebSocketConnection>();
        private ConcurrentDictionary<Guid, Guid> webSocketSystem = new ConcurrentDictionary<Guid, Guid>();

        public WebSocketManager()
        {
        }

        public ConcurrentDictionary<Guid, WebSocketConnection> GetWebSockets()
        {
            return webSockets;
        }

        public WebSocketConnection GetSocketById(Guid id)
        {
            return webSockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public Guid GetId(WebSocketConnection webSocketConnection)
        {
            return webSockets.FirstOrDefault(p => p.Value == webSocketConnection).Key;
        }

        public Guid GetSystemId(Guid webSocketConnectionId)
        {
            webSocketSystem.TryGetValue(webSocketConnectionId, out Guid result);
            return result;
        }

        public void AddSocket(Guid webSocketConnectionId, WebSocketConnection webSocketConnection)
        {
            webSockets.TryAdd(webSocketConnectionId, webSocketConnection);
        }

        public void SetSystemLink(Guid webSocketConnectionId, Guid SystemId)
        {
            webSocketSystem.TryAdd(webSocketConnectionId, SystemId);
        }

        public void RemoveSocket(Guid webSocketConnectionId)
        {
            webSockets.TryRemove(webSocketConnectionId, out WebSocketConnection webSocketConnection);
            webSocketSystem.TryRemove(webSocketConnectionId, out Guid systemId);
        }

    }
}