using Microsoft.AspNetCore.Builder;

namespace Syski.WebSocket.Services.WebSockets
{
    public static class WebSocketMiddlewareExtensions
    {

        public static IApplicationBuilder UseWebSocketMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<WebSocketMiddleware>();
            return app;
        }

    }
}
