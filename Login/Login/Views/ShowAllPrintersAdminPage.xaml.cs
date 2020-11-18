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

        async private void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Printer printer = (Printer)item.CommandParameter;

            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            CartItem cart = db.Table<CartItem>().Where(i=>i.printer_id == printer.printer_id).FirstOrDefault();

            if(cart != null)
            {
                await DisplayAlert("Cannot delete this printer", "Printer is either in someone's cart or was in someone's order", "OK");
                return;
            }

            var answer = await DisplayAlert("Are you sure?", "Deleting a printer cannot be undone.", "Yes", "No");

            if (answer)
            {
                db.Table<Printer>().Delete(p=>p.printer_id == printer.printer_id);
                init_list();
                await DisplayAlert("Printer deleted", null, "OK");
            }

            db.Close();
        }
    }
}