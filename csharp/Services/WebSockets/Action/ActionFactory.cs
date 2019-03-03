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
            }
            return result;
        }

    }
}
