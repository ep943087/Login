using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

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
            cart.ItemsSource = db.Table<CartItem>().Where(c => c.order_id == curr_order.order_id).ToList();
        }
    }
}