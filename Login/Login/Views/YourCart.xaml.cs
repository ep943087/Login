using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Login.Models;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourCart : ContentPage
    {
        User curr_user;
        public YourCart(User c_user)
        {
            curr_user = c_user;
            InitializeComponent();

            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            cart.ItemsSource = db.Table<CartItem>().Where(c=>c.user_id==curr_user.user_id && c.order_id == -1).ToList();
        }

        async private void order_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("ORDER PLACED", null, "OK");
        }
    }
}