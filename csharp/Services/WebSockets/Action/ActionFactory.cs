using csharp.Services.WebSockets.Action.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace csharp.Services.WebSockets.Action
{
    public class ActionFactory
    {

        public static ActionHandler createActionHandler(IServiceProvider serviceProvider, WebSocket webSocket, Action action)
        {
            ActionHandler result = null;
            switch (action.action)
            {
                case "user-authentication":
                    {
                        result = new UserAuthenticationHandler(serviceProvider, webSocket, action);
                        break;
                    }
                case "system-authentication":
                    {
                        result = new SystemAuthenticationHandler(serviceProvider, webSocket, action);
                        break;
                    }
                case "staticsystem":
                    {
                        result = new StaticSystemHandler(serviceProvider, webSocket, action);
                        break;
                    }
                case "staticcpu":
                    {
                        result = new StaticCPUHandler(serviceProvider, webSocket, action);
                        break;
                    }
                case "staticos":
                    {
                        result = new StaticOperatingSystemHandler(serviceProvider, webSocket, action);
                        break;
                    }
                case "staticram":
                    {
                        result = new StaticRAMHandler(serviceProvider, webSocket, action);
                        break;
                    }
                case "staticgpu":
                    {
                        result = new StaticGPUHandler(serviceProvider, webSocket, action);
                        break;
                    }
                case "staticmotherboard":
                    {
                        result = new StaticMotherboardHandler(serviceProvider, webSocket, action);
                        break;
                    }
                case "staticstorage":
                    {
                        result = new StaticStorageHandler(serviceProvider, webSocket, action);
                        break;
                    }
                default:
                    {
                        result = new DefaultHandler(serviceProvider, webSocket, action);
                        break;
                    }
            }
            return result;
        }

        public static Action createAction(string actionName)
        {
            return createAction(actionName, null);
        }

        public static Action createAction(string actionName, JObject actionProperties)
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
