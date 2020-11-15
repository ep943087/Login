using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Login.Models;
using SQLite;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserViewPrinters : ContentPage
    {
        public UserViewPrinters()
        {
            InitializeComponent();
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            List<Category> cats = db.Table<Category>().OrderBy(c => c.category_name).ToList();
            cats.Insert(0, new Category { category_id = -1, category_name = "Show All Printers" });
            category.ItemsSource = cats;

            db.Close();
            show_printers();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            show_printers();
        }
        private void show_printers()
        {
            Category cat = (Category)category.SelectedItem;
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            if(cat != null && cat.category_id == -1)
            {
                List<Printer> ps = db.Table<Printer>().Where(p => p.availableToPurchase).OrderBy(p => p.category_id).ToList();
                List<PrinterListItem> items = new List<PrinterListItem>();

                for (int i = 0; i < ps.Count; i++)
                {
                    items.Add(new PrinterListItem(ps[i]));
                }
                printers.ItemsSource = items;
            }
            else if(cat != null)
            {
                //printers.ItemsSource = 
                List<Printer> ps = db.Table<Printer>().Where(p => p.category_id==cat.category_id && p.availableToPurchase).ToList();
                List<PrinterListItem> items = new List<PrinterListItem>();

                for(int i = 0; i < ps.Count; i++)
                {
                    items.Add(new PrinterListItem(ps[i]));
                }
                printers.ItemsSource = items;
            }
            db.Close();
        }

        private void category_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            show_printers();
        }

        async private void printers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Printer p = (Printer)printers.SelectedItem;
            await Navigation.PushAsync(new UserViewPrinterInfo(p));
        }
    }
}