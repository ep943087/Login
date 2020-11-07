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
    public partial class UserHomePage : ContentPage
    {
        User curr_user;
        public UserHomePage(User c_user)
        {
            curr_user = c_user;
            InitializeComponent();
            admin.IsVisible = curr_user.isAdmin;
        }

        async private void printers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserViewPrinters(curr_user));
        }

        async private void cart_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new YourCart(curr_user));
        }

        async private void orders_Clicked(object sender, EventArgs e)
        {

        }

        async private void account_Clicked(object sender, EventArgs e)
        {

        }

        async private void admin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminHomePage(curr_user));
        }
    }
}