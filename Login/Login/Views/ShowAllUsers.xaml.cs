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
        private User curr_user;
        public ShowAllUsers(User user)
        {
            curr_user = user;
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            listView.ItemsSource = db.Table<User>().OrderBy(u => u.username).ToList();
            db.Close();
        }

        async private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            User euser = (User)e.SelectedItem;
            await Navigation.PushAsync(new EditUser(curr_user,euser));
            listView.SelectedItem = null;
        }
    }
}