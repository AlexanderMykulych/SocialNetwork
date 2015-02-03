using Social.Domain.DataBase;
using Social.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.WebSockets;
using Ninject;

namespace Social.WebUI.Controllers.api
{
    public static class Db
    {
        static NinjectFactory factory = new NinjectFactory();

        public static IUser_Db _db = new Users();
    }
    public class WsChatController : ApiController
    {
        public static IUser_Db _db = new Users();

        private static List<WebSocket> Listeners = new List<WebSocket>();
        
        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(ProcessWSChat);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        private async Task ProcessWSChat(AspNetWebSocketContext context)
        {
            
            WebSocket socket = context.WebSocket;
            if (!Listeners.Contains(socket))
                Listeners.Add(socket);
            while (true)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                


                WebSocketReceiveResult result = await socket.ReceiveAsync(
                    buffer, CancellationToken.None);
                if (socket.State == WebSocketState.Open)
                {
                    string userMessage = Encoding.UTF8.GetString(
                        buffer.Array, 0, result.Count);

                    
                    UserMessage msg = new UserMessage()
                                        {
                                            Message = userMessage.Split(new char[] { ':' })[1],
                                            UserName = userMessage.Split(new char[] { ':' })[0]
                                        };

                    //await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    _db.AddMessage(msg.UserName, msg.Message);

                    var count = 20;
                    var history = _db.THistory
                                        .Skip(_db.THistory.Count() - count)
                                        .Take(count)
                                        .ToList();

                    StringBuilder message = new StringBuilder(400);
                    //foreach (var item in history)
                    //{
                    //    message.Append(item.UserName + ":" + item.Message + ";");
                    //}

                    //buffer = new ArraySegment<byte>(
                    //    Encoding.UTF8.GetBytes(message.ToString()));

                    foreach (var listener in Listeners)
                    {
                        await listener.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
                else
                {
                    break;
                }
            }
        }

    }
}
