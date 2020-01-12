using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShiYuDB.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string picture { get; set; }
        public string time { get; set; }
        public string SpecialMark { get; set; }
    }
}