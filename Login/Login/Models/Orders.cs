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
        public int user_id { get; set; }
        public DateTime date { get; set; }

        public override string ToString()
        {
            return date.ToString();
        }

        public List<CartItem> get_cart()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            List<CartItem> cart = db.Table<CartItem>().Where(c => c.order_id == this.order_id).ToList();
            db.Close();
            return cart;
        }

        public float get_price()
        {
            float sum = 0;

            List<CartItem> cart = get_cart();
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            foreach(CartItem item in cart)
            {
                Printer printer = db.Table<Printer>().Where(p => p.printer_id == item.printer_id).First();
                sum += printer.get_total_price();
            }
            return sum;
        }

        static public List<Orders> get_all()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            db.Close();
            return db.Table<Orders>().OrderByDescending(o => o.date).ToList();
        }

        static public List<Orders> get_by_user(User user)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            return db.Table<Orders>().Where(o => o.user_id == user.user_id).OrderByDescending(o => o.date).ToList();
        }
        public bool PlaceOrder(User user)
        {
            List<CartItem> cart = user.get_cart();
            if(cart.Count == 0)
            {
                return false;
            }

            this.user_id = user.user_id;
            this.date = DateTime.UtcNow;

            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            db.Insert(this);
            
            foreach(CartItem item in cart)
            {
                item.order_id = this.order_id;
                db.Update(item);
            }
            db.Close();

            return true;
        }
    }
}
