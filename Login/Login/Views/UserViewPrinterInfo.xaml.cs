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
        User curr_user;
        Printer curr_printer;

        public UserViewPrinterInfo(User u,Printer p)
        {
            curr_user = u;
            curr_printer = p;
            InitializeComponent();

            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            printer_name.Text = curr_printer.printer_name;
            printer_price.Text = curr_printer.get_total_price().ToString("C", CultureInfo.CurrentCulture);
            printer_features.ItemsSource = db.Table<PrinterFeature>().Where(pf => pf.printer_id == curr_printer.printer_id).ToList();
            db.Close();

            determine_cart_btn_name();
        }

        private void determine_cart_btn_name()
        {
            bool in_cart = curr_printer.in_cart(curr_user);

            if (in_cart)
            {
                cart_btn.Text = "Remove from Cart";
                cart_btn.TextColor = Color.Red;
            }
            else
            {
                cart_btn.Text = "Add to Cart";
                cart_btn.TextColor = Color.Green;
            }
        }

        private void cart_btn_Clicked(object sender, EventArgs e)
        {
            bool in_cart = curr_printer.in_cart(curr_user);

            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            if (in_cart)
            {
                db.Table<CartItem>().Delete(c => c.order_id == -1 && c.printer_id == curr_printer.printer_id && c.user_id == curr_user.user_id);
            }
            else
            {
                CartItem item = new CartItem
                {
                    order_id = -1,
                    printer_id = curr_printer.printer_id,
                    user_id = curr_user.user_id
                };
                db.Insert(item);
            }

            determine_cart_btn_name();
        }
    }
}