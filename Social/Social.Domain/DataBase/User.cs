using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Social.Domain.DataBase
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        public List<Message> Messages { get; set; } 
    }
}
