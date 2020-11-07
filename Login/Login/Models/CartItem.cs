using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{
    public class CartItem
    {
        [PrimaryKey,AutoIncrement]
        public int cart_item_id { get; set; }
        public int user_id { get; set; }
        public int printer_id { get; set; }
        public int order_id { get; set; }
    }
}
