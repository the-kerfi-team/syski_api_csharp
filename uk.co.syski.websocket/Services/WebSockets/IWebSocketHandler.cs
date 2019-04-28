using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets
{
    public interface IWebSocketHandler
    {

        Task OnConnected(WebSocketConnection webSocketConnection);

        Task OnDisconnected(WebSocketConnection webSocketConnection);

        Task OnReceiveMessage(WebSocketConnection webSocketConnection);

    }
}
