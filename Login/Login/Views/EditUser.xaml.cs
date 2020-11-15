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
    public partial class EditUser : ContentPage
    {
        private User euser;
        public EditUser(User user)
        {
            InitializeComponent();
            address.Text = user.user_address;
            password.Text = user.password;
            username.Text = user.username;
            isAdmin.IsToggled = user.isAdmin;
            euser = user;
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            if(String.IsNullOrEmpty(username.Text) || username.Text.Length < 5)
            {
                await DisplayAlert("INVALID INPUT","Username needs at least 5 characters","OK");
                return;
            } else if (db.Table<User>().Where(c=>c.username==username.Text).FirstOrDefault() != null)
            {
                if(euser.username != username.Text)
                {
                    await DisplayAlert("INVALID INPUT", "Username is taken", "OK");
                    return;
                }
            }

            euser.username = username.Text;
            euser.isAdmin = isAdmin.IsToggled;

            if(App.curr_user.user_id == euser.user_id)
            {
                App.curr_user = euser;
            }

            db.Update(euser);

            await DisplayAlert(null, "User Updated", "OK");
            await Navigation.PopAsync();

            db.Close();
        }
    }
}