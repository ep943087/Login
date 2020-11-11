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
        User curr_user;
        public YourAccountPage(User c_user)
        {
            curr_user = c_user;
            InitializeComponent();

            username.Text = curr_user.username;
            password.Text = curr_user.password;
            address.Text = curr_user.user_address;
            card.Text = curr_user.user_card_num;
            security.Text = curr_user.securyity_num;
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
                curr_user.username = username.Text;
                curr_user.password = password.Text;
                curr_user.user_address = address.Text;
                curr_user.user_card_num = card.Text;
                curr_user.securyity_num = security.Text;
                SQLiteConnection db = new SQLiteConnection(App._dbPath);
                db.Update(curr_user);

                await DisplayAlert("Successful Update", "Your user information was updated", "OK");
            }
        }
    }
}