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
                        result = new StaticMotherboardHandler(serviceProvider, webSocket, action);
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
            return createAction(actionName, new JObject());
        }

        public static Action createAction(string actionName, JObject actionProperties)
        {
            Action result = null;
            if (actionProperties == null)
            {
                actionProperties = new JObject();
            }
            switch (actionName)
            {
                case "authentication":
                    {
                        result = new Action()
                        {
                            action = "authentication",
                            properties = actionProperties
                        };
                        break;
                    }
                case "staticsystem":
                    {
                        result = new Action()
                        {
                            action = "staticsystem",
                            properties = actionProperties
                        };
                        break;
                    }
                case "staticcpu":
                    {
                        result = new Action()
                        {
                            action = "staticcpu",
                            properties = actionProperties
                        };
                        break;
                    }
                case "staticgpu":
                    {
                        result = new Action()
                        {
                            action = "staticgpu",
                            properties = actionProperties
                        };
                        break;
                    }
                case "staticram":
                    {
                        result = new Action()
                        {
                            action = "staticram",
                            properties = actionProperties
                        };
                        break;
                    }
                case "staticmotherboard":
                    {
                        result = new Action()
                        {
                            action = "staticmotherboard",
                            properties = actionProperties
                        };
                        break;
                    }
                case "staticos":
                    {
                        result = new Action()
                        {
                            action = "staticos",
                            properties = actionProperties
                        };
                        break;
                    }
                case "staticstorage":
                    {
                        result = new Action()
                        {
                            action = "staticstorage",
                            properties = actionProperties
                        };
                        break;
                    }
                case "shutdown":
                    {
                        result = new Action()
                        {
                            action = "shutdown",
                            properties = actionProperties
                        };
                        break;
                    }
                case "restart":
                    {
                        result = new Action()
                        {
                            action = "restart",
                            properties = actionProperties
                        };
                        break;
                    }
                case "error":
                default:
                    {
                        result = new Action()
                        {
                            action = "error",
                            properties = actionProperties
                        };
                        break;
                    }
            }
            return result;
        }

    }
}
