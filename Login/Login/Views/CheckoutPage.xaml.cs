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
    public partial class CheckoutPage : ContentPage
    {
        public CheckoutPage()
        {
            InitializeComponent();
            address.Text = App.curr_user.user_address;
            card.Text = App.curr_user.user_card_num;
            security.Text = App.curr_user.securyity_num;

            update_info();
        }
        void update_info()
        {
            float item = App.curr_user.cart_price_total();
            float shipping = 4;
            float before_tax = item + shipping;
            float tax = before_tax * .09f;
            float after_tax = before_tax + tax;

            item_price.Text = item.ToString("C", System.Globalization.CultureInfo.CurrentCulture);
            shipping_price.Text = shipping.ToString("C", System.Globalization.CultureInfo.CurrentCulture);
            before_taxes.Text = before_tax.ToString("C", System.Globalization.CultureInfo.CurrentCulture);
            tax_total.Text = tax.ToString("C", System.Globalization.CultureInfo.CurrentCulture);
            after_taxes.Text = after_tax.ToString("C", System.Globalization.CultureInfo.CurrentCulture);
        }
        bool valid_input()
        {
            if (String.IsNullOrEmpty(address.Text))
            {
                DisplayAlert("INVALID INPUT", "Address must be filled", "OK");
                return false;
            } else if(String.IsNullOrEmpty(card.Text) || card.Text.Length != 16)
            {
                DisplayAlert("INVALID INPUT","Card must contain 16 digits","OK");
                return false;
            } else if(String.IsNullOrEmpty(security.Text) || security.Text.Length != 3)
            {
                DisplayAlert("INVALID INPUT", "Security number must be 3 digits", "OK");
                return false;
            }
            return true;
        }

        async private void place_order_Clicked(object sender, EventArgs e)
        {
            if (!valid_input())
                return;   
            Orders order = new Orders
            {
                shipping_address = address.Text
            };
            order.PlaceOrder(App.curr_user);

            DisplayAlert("Order Placed", "Expect Delivery in 2-3 days", "OK");
            await Navigation.PopAsync();
        }
    }
}