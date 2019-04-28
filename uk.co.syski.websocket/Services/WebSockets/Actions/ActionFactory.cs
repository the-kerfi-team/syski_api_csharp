using Newtonsoft.Json.Linq;
using Syski.WebSocket.Services.WebSockets.Actions.Handlers;
using System;

namespace Syski.WebSocket.Services.WebSockets.Actions
{
    public class ActionFactory
    {

        public static ActionHandler CreateActionHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider)
        {
            ActionHandler result = null;
            switch (action.action)
            {
                case "user-authentication":
                {
                    result = new UserAuthenticationHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "system-authentication":
                {
                    result = new SystemAuthenticationHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "staticsystem":
                {
                    result = new StaticSystemHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "staticcpu":
                {
                    result = new StaticCPUHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "staticram":
                {
                    result = new StaticRAMHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "staticgpu":
                {
                    result = new StaticGPUHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "staticstorage":
                {
                    result = new StaticStorageHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "staticmotherboard":
                {
                    result = new StaticMotherboardHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "staticbios":
                {
                    result = new StaticBIOSHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "staticos":
                {
                    result = new StaticOperatingSystemHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "variableping":
                {
                    result = new VariablePingHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "variablecpu":
                {
                    result = new VariableCPUHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "variableram":
                {
                    result = new VariableRAMHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "variablestorage":
                {
                    result = new VariableStorageHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                case "runningprocesses":
                {
                    result = new VariableRunningProcessesHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
                default:
                {
                    result = new DefaultHandler(action, webSocketConnection, serviceProvider);
                    break;
                }
            }
            return result;
        }

        public static ActionHandler CreateAuthActionHandler(Action action, WebSocketConnection webSocketConnection, IServiceProvider serviceProvider)
        {
            ActionHandler result = null;
            switch (action.action)
            {
                case "user-authentication":
                    {
                        result = new UserAuthenticationHandler(action, webSocketConnection, serviceProvider);
                        break;
                    }
                case "system-authentication":
                    {
                        result = new SystemAuthenticationHandler(action, webSocketConnection, serviceProvider);
                        break;
                    }
                default:
                    {
                        result = new DefaultHandler(action, webSocketConnection, serviceProvider);
                        break;
                    }
            }
            return result;
        }


        public static Action CreateAction(string actionName)
        {
            return CreateAction(actionName, null);
        }

        public static Action CreateAction(string actionName, JObject actionProperties)
        {
            if (actionProperties == null)
            {
                actionProperties = new JObject();
            }
            return new Action()
            {
                action = actionName,
                properties = actionProperties
            };
        }

    }
}
