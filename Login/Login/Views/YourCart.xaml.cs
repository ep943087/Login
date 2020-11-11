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
            update_info();
        }

        void update_info()
        {
            current_price.Text = curr_user.cart_price_total().ToString("C", System.Globalization.CultureInfo.CurrentCulture);

            update_cart_list();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            update_info();
        }
        private void update_cart_list()
        {
            //SQLiteConnection db = new SQLiteConnection(App._dbPath);
            //cart.ItemsSource = db.Table<CartItem>().Where(c => c.user_id == curr_user.user_id && c.order_id == -1).ToList();
            cart.ItemsSource = curr_user.get_cart_list_items();
        }
        async private void order_Clicked(object sender, EventArgs e)
        {
            if (curr_user.at_least_one_item())
            {
                await Navigation.PushAsync(new CheckoutPage(curr_user));
            }
            else
            {
                await DisplayAlert("No Items In Cart", "Order was not placed", "OK");
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}