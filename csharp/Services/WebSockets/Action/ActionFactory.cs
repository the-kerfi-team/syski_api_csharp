using csharp.Services.WebSockets.Action.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace csharp.Services.WebSockets.Action
{
    public class ActionFactory
    {

        public static ActionHandler createActionHandler(WebSocket webSocket, Guid? systemId, Action action)
        {
            ActionHandler result = null;
            switch (action.action)
            {
                case "user-authentication":
                    {
                        result = new UserAuthenticationHandler(webSocket, systemId, action);
                        break;
                    }
                case "system-authentication":
                    {

                        break;
                    }
                case "staticsystem":
                    {
                        result = new StaticSystemHandler(webSocket, systemId, action);
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
                            properties = new Dictionary<string, string>()
                        };
                        break;
                    }
                case "staticsystem":
                    {
                        result = new Action()
                        {
                            action = "staticsystem",
                            properties = new Dictionary<string, string>()
                        };
                        break;
                    }
            }
            return result;
        }

    }
}
