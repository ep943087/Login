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
            printer_price.Text = curr_printer.additional_cost.ToString();
            db.Close();
        }
    }
}