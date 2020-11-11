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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            curr_user = db.Table<User>().Where(u => u.user_id == curr_user.user_id).First();
            welcome.Text = "Welcome, " + curr_user.username;
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
            await Navigation.PushAsync(new YourOrderPage(curr_user));
        }

        async private void account_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new YourAccountPage(curr_user));
        }

        async private void admin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminHomePage(curr_user));
        }
    }
}