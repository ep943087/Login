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
        public YourCart()
        {
            InitializeComponent();
            update_info();
        }

        void update_info()
        {
            current_price.Text = App.curr_user.cart_price_total().ToString("C", System.Globalization.CultureInfo.CurrentCulture);
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
            cart.ItemsSource = App.curr_user.get_cart_list_items();
        }
        async private void order_Clicked(object sender, EventArgs e)
        {
            if (App.curr_user.at_least_one_item())
            {
                await Navigation.PushAsync(new CheckoutPage());
            }
            else
            {
                await DisplayAlert("No Items In Cart", "Order was not placed", "OK");
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            CartItem item = (CartItem)menu.CommandParameter;
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            db.Table<CartItem>().Delete(c=>c.cart_item_id == item.cart_item_id);
            update_info();
        }

        async private void cart_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CartItem item = (CartItem)e.SelectedItem;
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            Printer printer = db.Table<Printer>().Where(p => p.printer_id == item.printer_id).First();
            await Navigation.PushAsync(new UserViewPrinterInfo(printer));
        }
    }
}