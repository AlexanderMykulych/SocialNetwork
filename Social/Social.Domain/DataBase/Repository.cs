using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain.DataBase
{
    public class Repository
    {
        static Users _db = new Users();
        public static Users getRepository()
        {
            return _db;
        }
    }
}
