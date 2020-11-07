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
    public partial class AddFeature : ContentPage
    {
        private User curr_user;
        public AddFeature(User c_user)
        {
            curr_user = c_user;
            InitializeComponent();
        }

        async private void submit_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            float price = 0.0f;

            try
            {
                price = float.Parse(feature_price.Text);
            }
            catch(FormatException)
            {
                await DisplayAlert("INVALID", "Price must be a number", "OK");
                return;
            }
            catch (OverflowException)
            {
                await DisplayAlert("INVALID", "Price must be a number", "OK");
                return;
            }

            Feature newFeature = new Feature
            {
                feature_description = feature_description.Text,
                feature_price = float.Parse(feature_price.Text)
            };

            db.Insert(newFeature);

            db.Close();

            await DisplayAlert("Added New Feature", feature_description.Text = " was added", "OK");
            await Navigation.PopAsync();
        }
    }
}