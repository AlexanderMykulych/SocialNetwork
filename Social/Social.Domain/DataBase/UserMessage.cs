using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.DataBase
{
    public class UserMessage
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
