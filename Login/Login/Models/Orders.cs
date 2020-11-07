using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{
    public class Orders
    {
        [PrimaryKey, AutoIncrement]
        public int order_id { get; set; }
        public DateTime date { get; set; }
    }
}
