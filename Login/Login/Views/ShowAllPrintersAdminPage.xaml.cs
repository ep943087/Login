using Login.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowAllPrintersAdminPage : ContentPage
    {
        public ShowAllPrintersAdminPage()
        {
            InitializeComponent();
            init_list();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            init_list();
        }
        private void init_list()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            List<Printer> ps = db.Table<Printer>().ToList();
            List<PrinterListItem> ps_list = new List<PrinterListItem>();

            foreach(Printer p in ps)
            {
                ps_list.Add(new PrinterListItem(p));
            }

            listView.ItemsSource = ps_list;
        }
        async private void add_printer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPrinter(null));
        }

        async private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Printer item = (Printer)e.SelectedItem;

            await Navigation.PushAsync(new AddPrinter(item));
        }
    }
}