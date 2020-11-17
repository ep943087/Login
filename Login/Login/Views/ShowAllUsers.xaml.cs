using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Login.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowAllUsers : ContentPage
    {
        public ShowAllUsers()
        {
            InitializeComponent();

        }


        void update_list()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            listView.ItemsSource = db.Table<User>().OrderBy(u => u.username).ToList();
            db.Close();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            update_list();
        }

        async private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            User euser = (User)e.SelectedItem;
            await Navigation.PushAsync(new EditUser(euser));
        }

        async private void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            User selected_user = (User)item.CommandParameter;

            if(selected_user.user_id == App.curr_user.user_id)
            {
                await DisplayAlert("Failed to delete", "You cannot delete your own account in admin", "OK");
                return;
            }
            var answer = await DisplayAlert("Deleting","Are you sure you want to delete this user?","Yes","No");

            if (answer)
            {
                selected_user.delete_me();
                await DisplayAlert("Deleted", null, "OK");
                update_list();
            }
        }
    }
}