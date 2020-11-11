using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{

    public class CartItemListItem : CartItem
    {
        public string image { get; set; }
        public string name { get; set; }
        public float total { get; set; }
        public string company { get; set; }
        public string category { get; set; }
        public CartItemListItem(CartItem item)
        {
            this.cart_item_id = item.cart_item_id;
            this.user_id = item.user_id;
            this.printer_id = item.printer_id;
            this.order_id = item.order_id;
            this.cart_price = item.cart_price;
            this.count = item.count;
            
            // additional information for list
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            Printer p = db.Table<Printer>().Where(t => t.printer_id == this.printer_id).First();
            PrinterListItem p2 = new PrinterListItem(p);
            this.name = p2.printer_name;
            this.image = p2.image;
            this.total = this.count * p2.printer_price;
            this.company = p2.company_name;
            this.category = db.Table<Category>().Where(c => c.category_id == p2.category_id).First().category_name;

            db.Close();
        }
    }
    public class CartItem
    {
        [PrimaryKey,AutoIncrement]
        public int cart_item_id { get; set; }
        public int user_id { get; set; }
        public int printer_id { get; set; }
        public int order_id { get; set; }
        public float cart_price { get; set; }
        public int count { get; set; }

        public override string ToString()
        {
            return get_printer().ToString();
        }
        public Printer get_printer()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            return db.Table<Printer>().Where(p => p.printer_id == this.printer_id).First();
        }
    }
}
