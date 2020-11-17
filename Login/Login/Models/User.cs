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
        public string user_address { get; set; }
        public string user_card_num { get; set; }
        public string securyity_num { get; set; }

        public void delete_me()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            // delete all orders and cart items of this user
            db.Table<Orders>().Delete(o => o.user_id == this.user_id);
            db.Table<CartItem>().Delete(item => item.user_id == this.user_id);
            db.Table<User>().Delete(u => u.user_id == this.user_id);
        }
        public bool at_least_one_item()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            int cnt = db.Table<CartItem>().Where(c => c.user_id == this.user_id && c.order_id == -1).Count();
            db.Close();
            return cnt > 0;
        }

        public List<CartItemListItem> get_cart_list_items()
        {
            List<CartItem> cart = get_cart();
            List<CartItemListItem> cart_list = new List<CartItemListItem>();
            foreach (CartItem item in cart)
            {
                cart_list.Add(new CartItemListItem(item));
            }

            return cart_list;
        }
        public List<CartItem> get_cart()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            return db.Table<CartItem>().Where(c => c.user_id == this.user_id && c.order_id == -1).ToList();
        }

        public float cart_price_total()
        {
            float sum = 0;

            List<CartItem> cart = get_cart();

            foreach(CartItem item in cart)
            {
                sum += item.cart_price * item.count;
            }

            return sum;
        }
    }
}
