using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.DataBase
{
    public interface IUser_Db
    {
        IQueryable<User> TUsers { get;}

        IQueryable<Message> TMessage { get;}

        IQueryable<UserMessage> THistory { get; }

        void AddUser(string UserName);

        void AddMessage(string UserName, string MessageText);

        void AddToHistory(UserMessage newMessage, bool SaveChange = true);
    }
}
