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
    public partial class YourAccountPage : ContentPage
    {
        public YourAccountPage()
        {
            InitializeComponent();

            username.Text = App.curr_user.username;
            password.Text = App.curr_user.password;
            address.Text = App.curr_user.user_address;
            card.Text = App.curr_user.user_card_num;
            security.Text = App.curr_user.securyity_num;
        }

        async private void Update_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(username.Text) || username.Text.Length < 5) 
            {
                await DisplayAlert("INVALID INPUT","Username must be at least 5 characters","OK");
            } else if(String.IsNullOrEmpty(password.Text) || password.Text.Length < 5)
            {
                await DisplayAlert("INVALID INPUT", "Password must be at least 5 characters", "OK");
            }/*
            else if (String.IsNullOrEmpty(address.Text))
            {
                await DisplayAlert("INVALID INPUT", "Address must be filled", "OK");
            }*/
            else if(!String.IsNullOrEmpty(card.Text) && card.Text.Length != 16)
            {
                await DisplayAlert("INVALID INPUT", "Card number must be 16 characters", "OK");
            }
            else if(!String.IsNullOrEmpty(security.Text) && security.Text.Length != 3)
            {
                await DisplayAlert("INVALID INPUT", "Security number must be 3 characters", "OK");
            }
            else
            {
                App.curr_user.username = username.Text;
                App.curr_user.password = password.Text;
                App.curr_user.user_address = address.Text;
                App.curr_user.user_card_num = card.Text;
                App.curr_user.securyity_num = security.Text;
                SQLiteConnection db = new SQLiteConnection(App._dbPath);
                db.Update(App.curr_user);

                await DisplayAlert("Successful Update", "Your user information was updated", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}