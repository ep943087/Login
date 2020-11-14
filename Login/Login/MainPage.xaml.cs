using Login.Models;
using Login.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Login
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.curr_user = null;
            checkAdmin();
        }

        private void checkAdmin()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            User admin = db.Table<User>().Where(c => c.isAdmin).FirstOrDefault();

            if(admin == null)
            {
                admin = db.Table<User>().Where(c => c.username=="eliasproctor").FirstOrDefault();
                if(admin == null) 
                {
                    admin = new User
                    {
                        username = "eliasproctor",
                        password = "12345",
                        user_address = "1002 NW Gore Blvd",
                        user_card_num = "1234567890123456",
                        securyity_num = "123",
                        isAdmin = true
                    };
                    db.Insert(admin);
                }
                else
                {
                    admin.isAdmin = true;
                    db.Update(admin);
                }

            }
            db.Close();
        }
        async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {

            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            App.curr_user = db.Table<User>().Where((c)=>
                c.username == username.Text && c.password == password.Text
            ).FirstOrDefault();

            if (App.curr_user == null)
            {
                await DisplayAlert("ERROR", "Username or password is incorrect", "Try, Again");
            }
            else
            {
                username.Text = password.Text = "";
                await DisplayAlert("SUCCESS", "YOU LOGGED IN SUCCESSFULLY", "OK");
                await Navigation.PushAsync(new Views.UserHomePage());
            }
            db.Close();
        }
    }
}
