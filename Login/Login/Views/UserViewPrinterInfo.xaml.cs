using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Login.Models;
using System.Globalization;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserViewPrinterInfo : ContentPage
    {
        Printer curr_printer;
        public UserViewPrinterInfo(Printer p)
        {
            curr_printer = p;
            InitializeComponent();

            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            printer_name.Text = curr_printer.printer_name;
            printer_price.Text = curr_printer.get_total_price().ToString("C", CultureInfo.CurrentCulture);
            features.Text = curr_printer.features;
            image.Source = (new PrinterListItem(curr_printer)).image;
            printer_company.Text = curr_printer.company_name;
            db.Close();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            determine_cart_btn_name();
        }

        private void determine_cart_btn_name()
        {
            if(App.curr_user == null)
            {
                not_logged_in.IsVisible = true;
                user_logged.IsVisible = false;
                return;
            }
            else
            {
                not_logged_in.IsVisible = false;
                user_logged.IsVisible = true;
                
            }
            bool in_cart = curr_printer.in_cart(App.curr_user);

            if (in_cart)
            {
                cart_btn.Text = "Remove from Cart";
                cart_btn.TextColor = Color.Red;
                printer_count.IsVisible = true;
                how_many.IsVisible = true;

                for(int i = 0; i < printer_count.ItemsSource.Count; i++)
                {
                    if (int.Parse(printer_count.ItemsSource[i].ToString()) == curr_printer.get_cart_item().count)
                    {
                        printer_count.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                cart_btn.Text = "Add to Cart";
                cart_btn.TextColor = Color.Green;
                printer_count.IsVisible = false;
                how_many.IsVisible = false;
            }
        }

        private void cart_btn_Clicked(object sender, EventArgs e)
        {
            bool in_cart = curr_printer.in_cart(App.curr_user);

            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            if (in_cart)
            {
                db.Table<CartItem>().Delete(c => c.order_id == -1 && c.printer_id == curr_printer.printer_id && c.user_id == App.curr_user.user_id);
            }
            else
            {
                CartItem item = new CartItem
                {
                    order_id = -1,
                    printer_id = curr_printer.printer_id,
                    user_id = App.curr_user.user_id,
                    cart_price = curr_printer.printer_price,
                    count = 1,
                };
                db.Insert(item);
            }

            determine_cart_btn_name();
        }

        async private void printer_count_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newCount = int.Parse(printer_count.SelectedItem.ToString());

            CartItem item = curr_printer.get_cart_item();
            item.count = newCount;

            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            db.Update(item);

            //await DisplayAlert("You have " + newCount + " of these printers in your cart",null,"OK");
        }
    }
}