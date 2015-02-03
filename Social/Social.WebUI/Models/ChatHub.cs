using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Social.Domain.DataBase;

namespace Social.WebUI.Models
{
    public class ChatHub : Hub
    {
        static IUser_Db _db = new Users();

        static List<listener> users = new List<listener>();
        public void Send(string name, string message)
        {
            _db.AddMessage(name, message);

            Clients.All.addMessage(name, message);
        }

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!users.Any(x => x.Connectionid == id))
            {
                users.Add(new listener() { Connectionid = id, Name = userName });

                Clients.Caller.onConnected(id, userName, users);

                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = users.FirstOrDefault(x => x.Connectionid == Context.ConnectionId);

            if (item != null)
            {
                users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}