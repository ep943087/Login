using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using Xamarin.Forms;

namespace Login.Models
{
    public class Printer
    {
        [PrimaryKey, AutoIncrement]
        public int printer_id { get; set; }
        public string printer_name { get; set; }
        public float additional_cost { get; set; }
        public int category_id { get; set; }

        [DefaultValue(value: false)]
        public bool isColored { get; set; }
        [DefaultValue(value: false)]
        public bool availableToPurchase { get; set; }
        public string companyName { get; set; }

        public string get_category_name()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            Category cat = db.Table<Category>().Where(c => c.category_id == this.category_id).FirstOrDefault();
            return cat == null ? "No Category" : cat.category_name;
        }

        public bool in_cart(User user)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            CartItem item = db.Table<CartItem>().Where(c => c.order_id == -1 && c.printer_id == this.printer_id && c.user_id == user.user_id).FirstOrDefault();
            db.Close();
            return item != null;
        }

        public override string ToString()
        {
            return printer_name;
        }
    }
}
