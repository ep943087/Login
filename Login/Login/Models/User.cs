﻿using System;
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

        public List<CartItem> get_cart()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            return db.Table<CartItem>().Where(c => c.user_id == this.user_id && c.order_id == -1).ToList();
        }
    }
}
