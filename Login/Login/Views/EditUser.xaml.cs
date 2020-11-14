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
            BindingContext = user;
            euser = user;
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            euser.username = username.Text;
            euser.isAdmin = isAdmin.IsToggled;

            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            db.Update(euser);

            await DisplayAlert(null, "User Updated", "OK");
            await Navigation.PopAsync();

            db.Close();
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            db.Table<User>().Delete(u=>u.user_id == euser.user_id);

            db.Close();

            await DisplayAlert("Deleted " + euser.username,null,"OK");
            await Navigation.PopAsync();
        }
    }
}