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
    public partial class ShowFeaturesPage : ContentPage
    {
        private User curr_user;
        public ShowFeaturesPage(User c_user)
        {
            curr_user = c_user;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            db.CreateTable<Feature>();

            listView.ItemsSource = db.Table<Feature>().OrderBy(f=>f.feature_description).ToList();

            db.Close();
        }

        async private void add_feature_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddFeature(curr_user));
        }
    }
}