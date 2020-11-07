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
    public partial class AddPrinterFeaturePage : ContentPage
    {
        User curr_user;
        Printer edit_printer;
        public AddPrinterFeaturePage(User c_user,Printer edit)
        {
            InitializeComponent();
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            features.ItemsSource = db.Table<Feature>().OrderBy(f => f.feature_price).ToList();
            db.Close();

            edit_printer = edit;
            curr_user = c_user;
        }

        async private void submit_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            Feature feature = (Feature)features.SelectedItem;

            if(feature == null)
            {
                await DisplayAlert("INVALID", "Feature is required", "OK");
                return;
            }

            PrinterFeature pf = new PrinterFeature
            {
                printer_id = edit_printer.printer_id,
                feature_id = feature.feature_id,
            };

            db.Insert(pf);

            db.Close();
            await Navigation.PopAsync();
        }
    }
}