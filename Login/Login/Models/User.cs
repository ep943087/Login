using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Login.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
    }
}
