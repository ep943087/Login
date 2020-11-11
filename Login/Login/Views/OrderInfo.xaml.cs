using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Data.Common;
using System.Globalization;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderInfo : ContentPage
    {
        Orders curr_order;
        public OrderInfo(Orders order)
        {
            curr_order = order;
            InitializeComponent();
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            //cart.ItemsSource = db.Table<CartItem>().Where(c => c.order_id == curr_order.order_id).ToList();
            cart.ItemsSource = order.get_cart_list();
            price.Text = order.get_price_after_tax().ToString("C", CultureInfo.CurrentCulture);
            order_date.Text = order.date.ToString();
            CartItem item = db.Table<CartItem>().Where(c => c.order_id == order.order_id).First();
            User user = db.Table<User>().Where(u => item.user_id == u.user_id).First();
            username.Text = user.username;
            address.Text = order.shipping_address;
        }
    }
}