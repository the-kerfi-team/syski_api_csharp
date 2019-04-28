using Microsoft.Extensions.Hosting;
using Syski.Data;
using Syski.WebSocket.Services.WebSockets.Actions.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Syski.WebSocket.Services.WebSockets
{
    public class WebSocketTaskScheduler : IHostedService
    {

        private readonly WebSocketManager webSocketManager;
        private readonly IServiceProvider serviceProvider;
        private List<Timer> timers = new List<Timer>();

        public WebSocketTaskScheduler(WebSocketManager webSocketManager, IServiceProvider serviceProvider)
        {
            this.webSocketManager = webSocketManager;
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timers.Add(new Timer(SendAction, new CommandTask(serviceProvider), TimeSpan.Zero, TimeSpan.FromSeconds(1)));
            timers.Add(new Timer(SendAction, new VariablePingTask(serviceProvider), TimeSpan.Zero, TimeSpan.FromSeconds(10)));
            timers.Add(new Timer(SendAction, new DefaultTask("variablecpu", serviceProvider), TimeSpan.Zero, TimeSpan.FromSeconds(3)));
            timers.Add(new Timer(SendAction, new DefaultTask("variableram", serviceProvider), TimeSpan.Zero, TimeSpan.FromSeconds(3)));
            timers.Add(new Timer(SendAction, new DefaultTask("variablestorage", serviceProvider), TimeSpan.Zero, TimeSpan.FromSeconds(3)));
            //timers.Add(new Timer(SendAction, new DefaultTask("variablenetwork", serviceProvider), TimeSpan.Zero, TimeSpan.FromSeconds(3)));
            timers.Add(new Timer(SendAction, new DefaultTask("processes", serviceProvider), TimeSpan.Zero, TimeSpan.FromSeconds(30)));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (Timer timer in timers)
            {
                timer?.Change(Timeout.Infinite, 0);
            }
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            foreach (Timer timer in timers)
            {
                timer?.Dispose();
            }
        }

        private void SendAction(object state)
        {
            ActionTask actionTask = (ActionTask) state;
            foreach (var webSocketConnection in webSocketManager.GetWebSockets())
            {
                try
                {
                    actionTask.ExecuteActionTask(webSocketConnection.Value);
                }
                catch
                {

                }
            }
        }

    }
}
