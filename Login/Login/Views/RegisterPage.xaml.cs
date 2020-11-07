using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Login.Models;
namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        async private void submit_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            //db.CreateTable<User>();

            if (string.IsNullOrWhiteSpace(username.Text) || string.IsNullOrWhiteSpace(password.Text) || username.Text.Length <= 4 || password.Text.Length <= 4) 
            {
                await DisplayAlert("INVALID","Username and password must be at least 5 characters","OK");
                return;
            }
            User searchUser = db.Table<User>().Where(user => user.username == username.Text).FirstOrDefault();
            if(searchUser != null)
            {
                await DisplayAlert("INVALID", "Username is taken", "OK");
                return;
            }
            User newUser = new User()
            {
                username = username.Text,
                password = password.Text,
                isAdmin = false
            };
            db.Insert(newUser);
            db.Close();

            await DisplayAlert("SUCCESS", "Registered " + username.Text, "OK");
            await Navigation.PopAsync();
        }
    }
}