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

                        break;
                    }
                case "staticsystem":
                    {
                        result = new StaticSystemHandler(serviceProvider, webSocket, action);
                        break;
                    }
            }
            return result;
        }

        public static Action createAction(string actionName)
        {
            Action result = null;
            switch (actionName)
            {
                case "authentication":
                    {
                        result = new Action()
                        {
                            action = "authentication",
                            properties = new JObject()
                        };
                        break;
                    }
                case "staticsystem":
                    {
                        result = new Action()
                        {
                            action = "staticsystem",
                            properties = new JObject()
                        };
                        break;
                    }
            }
            return result;
        }

    }
}
