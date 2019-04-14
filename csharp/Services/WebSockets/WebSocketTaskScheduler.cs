using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Microsoft.Extensions.Hosting;

namespace csharp.Services.WebSockets
{
    public class WebSocketTaskScheduler : IHostedService
    {

        private readonly WebSocketManager _webSocketManager;
        private List<Timer> _timers = new List<Timer>();

        public WebSocketTaskScheduler(WebSocketManager webSocketManager)
        {
            _webSocketManager = webSocketManager;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timers.Add(new Timer(SendAction, "variablecpu", TimeSpan.Zero, TimeSpan.FromSeconds(3)));
            _timers.Add(new Timer(SendAction, "variableram", TimeSpan.Zero, TimeSpan.FromSeconds(3)));
            _timers.Add(new Timer(SendAction, "variablestorage", TimeSpan.Zero, TimeSpan.FromSeconds(3)));
            _timers.Add(new Timer(SendAction, "variablenetwork", TimeSpan.Zero, TimeSpan.FromSeconds(3)));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (Timer timer in _timers)
            {
                timer?.Change(Timeout.Infinite, 0);
            }
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            foreach (Timer timer in _timers)
            {
                timer?.Dispose();
            }
        }

        private void SendAction(object state)
        {
            string action = (string) state;
            foreach (var webSocketConnection in _webSocketManager.GetWebSockets())
            {
                 webSocketConnection.Value.sendAction(action);
            }
        }

    }
}
