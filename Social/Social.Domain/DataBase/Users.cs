using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.DataBase
{
    public class Users : DbContext, IUser_Db
    {
        public Users() :base("DefaultConnection")
        {

        }
        public DbSet<User> UsersTable {get;set;}

        public DbSet<Message> MessageTable { get; set; }

        public DbSet<UserMessage> MessageHistory { get; set; }

        public IQueryable<User> TUsers
        {
            get
            {
                return UsersTable;
            }
        }

        public IQueryable<Message> TMessage
        {
            get
            {
                return MessageTable;
            }
        }
        public IQueryable<UserMessage> THistory
        {
            get
            {
                return MessageHistory;
            }
        }
        public void AddUser(string UserName)
        {
            if (UsersTable.FirstOrDefault(x => x.UserName == UserName) != null)
                return;
            UsersTable.Add(new User() { UserName = UserName });
            SaveChanges();
        }

        public void AddMessage(string UserName, string MessageText)
        {
            var _user = UsersTable.FirstOrDefault(x => x.UserName == UserName);

            if (_user != null)
            {
                MessageTable.Add(new Message() { Text = MessageText, user = _user });
                AddToHistory(new UserMessage() { Message = MessageText, UserName = _user.UserName }, false);
                SaveChanges();
            }
        }

        public void AddToHistory(UserMessage newMessage, bool SaveChange = true)
        {
            MessageHistory.Add(newMessage);
            if(SaveChange)
                SaveChanges();
        }
    }
}
