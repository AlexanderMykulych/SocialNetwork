using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Social.Domain.DataBase
{
    public class Message
    {

        public int id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
    }
}
