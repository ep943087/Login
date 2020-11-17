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
        public UserHomePage()
        {
            InitializeComponent();
            admin.IsVisible = App.curr_user.isAdmin;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            App.curr_user = db.Table<User>().Where(u => u.user_id == App.curr_user.user_id).First();
            welcome.Text = "Welcome, " + App.curr_user.username;
        }

        async private void cart_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new YourCart());
        }

        async private void orders_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new YourOrderPage());
        }

        async private void account_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new YourAccountPage());
        }

        async private void admin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminHomePage());
        }

        async private void logout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}