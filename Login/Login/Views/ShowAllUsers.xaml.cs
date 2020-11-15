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
            await Navigation.PushAsync(new EditUser(euser));
            listView.SelectedItem = null;
        }

        async private void MenuItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("NOTHING HAPPENED", null, "OK");
        }
    }
}