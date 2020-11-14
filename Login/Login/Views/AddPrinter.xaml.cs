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
        private Printer edit_printer;
        public AddPrinter(Printer edit)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            if(edit == null)
            {
                Printer maxid = db.Table<Printer>().OrderByDescending(c=>c.printer_id).FirstOrDefault();
                int id = maxid == null ? 1 : maxid.printer_id + 1;
                edit = new Printer
                {
                    printer_name = "No Name " + id,
                    category_id = -1,
                    printer_price = 0,
                    company_name = "",
                    availableToPurchase = false,
                    features = "",
                };

                db.Insert(edit);
            }

            edit_printer = edit;
            InitializeComponent();
            List<Category> cats = db.Table<Category>().OrderBy(c => c.category_name).ToList();
            categories.ItemsSource = cats;
            printer_company.Text = edit_printer.company_name;
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
            features.Text = edit_printer.features;
            total_price.Text = edit_printer.get_total_price().ToString("C", CultureInfo.CurrentCulture);
            printer_price.Text = edit_printer.printer_price.ToString();
            curr_company.Text = edit_printer.company_name;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async private void submit_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            Category cat = (categories.SelectedItem as Category);
            int cat_id = cat == null ? edit_printer.category_id : cat.category_id;

            edit_printer.printer_name = printer_name.Text;
            edit_printer.category_id = cat_id;
            edit_printer.printer_price = float.Parse(printer_price.Text);
            edit_printer.availableToPurchase = available.IsToggled;
            edit_printer.features = features.Text;
            edit_printer.company_name = printer_company.Text;
            db.Update(edit_printer);
            db.Close();
            set_title();
            await DisplayAlert("UPDATED","Printer info was saved","OK");
        }

    }
}