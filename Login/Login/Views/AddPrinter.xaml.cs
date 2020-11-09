using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Login.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPrinter : ContentPage
    {
        private User curr_user;
        private Printer edit_printer;
        public AddPrinter(User c_user, Printer edit)
        {
            curr_user = c_user;

            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            if(edit == null)
            {
                Printer maxid = db.Table<Printer>().OrderByDescending(c=>c.printer_id).FirstOrDefault();
                int id = maxid == null ? 1 : maxid.printer_id + 1;
                edit = new Printer
                {
                    printer_name = "No Name " + id,
                    category_id = -1,
                    additional_cost = 0,
                    companyName = "",
                    availableToPurchase = false,
                };

                db.Insert(edit);
            }

            edit_printer = edit;
            InitializeComponent();
            List<Category> cats = db.Table<Category>().OrderBy(c => c.category_name).ToList();
            categories.ItemsSource = cats;
            for(int i = 0; i < cats.Count; i++)
            {
                if(cats[i].category_id == edit.category_id)
                {
                    categories.SelectedIndex = i;
                    break;
                }
            }
            set_title();

            available.IsToggled = edit_printer.availableToPurchase;

            db.Close();

        }

        private void set_title()
        {
            curr_name.Text = printer_name.Text = edit_printer.printer_name;
            curr_cat.Text = edit_printer.get_category_name();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            printer_features.ItemsSource = db.Table<PrinterFeature>().Where(pf=>pf.printer_id==edit_printer.printer_id).ToList();

            feature_price.Text = edit_printer.get_feature_price().ToString("C", CultureInfo.CurrentCulture);
            total_price.Text = edit_printer.get_total_price().ToString("C", CultureInfo.CurrentCulture);
            additional_cost.Text = edit_printer.additional_cost.ToString();
        }

        async private void submit_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            Category cat = (categories.SelectedItem as Category);
            int cat_id = cat == null ? edit_printer.category_id : cat.category_id;

            edit_printer.printer_name = printer_name.Text;
            edit_printer.category_id = cat_id;
            edit_printer.availableToPurchase = available.IsToggled;
            db.Update(edit_printer);
            db.Close();
            set_title();
            await DisplayAlert("UPDATED","Printer info was saved","OK");
        }

        async private void add_feature_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPrinterFeaturePage(curr_user, edit_printer));
        }
    }
}