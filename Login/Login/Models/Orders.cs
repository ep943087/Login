using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Login.Models
{

    public class OrdersListItem: Orders
    {
        public float total_price { get; set; }
        public string username { get; set; }
        public OrdersListItem(Orders o)
        {
            this.order_id = o.order_id;
            this.user_id = o.user_id;
            this.date = o.date;
            this.shipping_address = o.shipping_address;
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            CartItem item = db.Table<CartItem>().Where(c => c.order_id == this.order_id).First();
            User user = db.Table<User>().Where(u => u.user_id == item.user_id).First();
            this.username = user.username;
            this.total_price = this.get_price_after_tax();
            db.Close();
        }
    }
    public class Orders
    {
        [PrimaryKey, AutoIncrement]
        public int order_id { get; set; }
        public int user_id { get; set; }
        public DateTime date { get; set; }
        public string shipping_address { get; set; }
        public override string ToString()
        {
            return date.ToString();
        }
        public static List<OrdersListItem> get_all_orders_list()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            List<Orders> orders = db.Table<Orders>().OrderByDescending(o => o.date).ToList();
            List<OrdersListItem> orders_list = new List<OrdersListItem>();
            foreach(Orders order in orders)
            {
                orders_list.Add(new OrdersListItem(order));
            }
            return orders_list;
        }

        public float get_price_after_tax()
        {
            float price = get_price();
            price += 4.0f;

            return price * 1.09f;
        }
        public List<CartItem> get_cart()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            List<CartItem> cart = db.Table<CartItem>().Where(c => c.order_id == this.order_id).ToList();
            db.Close();
            return cart;
        }

        public List<CartItemListItem> get_cart_list()
        {
            List<CartItem> cart = get_cart();
            List<CartItemListItem> cart_list = new List<CartItemListItem>();

            foreach(CartItem item in cart)
            {
                cart_list.Add(new CartItemListItem(item));
            }

            return cart_list;
        }

        public float get_price()
        {
            float sum = 0;

            List<CartItem> cart = get_cart();
            foreach(CartItem item in cart)
            {
                sum += item.cart_price * item.count;
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

        static public List<OrdersListItem> get_by_user_list(User user)
        {
            List<Orders> orders = get_by_user(user);
            List<OrdersListItem> orders_list = new List<OrdersListItem>();

            foreach(Orders order in orders)
            {
                orders_list.Add(new OrdersListItem(order));
            }

            return orders_list;
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
