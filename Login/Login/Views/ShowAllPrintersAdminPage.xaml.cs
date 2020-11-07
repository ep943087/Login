using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Login.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowAllPrintersAdminPage : ContentPage
    {
        private User curr_user;
        public ShowAllPrintersAdminPage(User c_user)
        {
            curr_user = c_user;
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
            listView.ItemsSource = db.Table<Printer>().ToList();
        }
        async private void add_printer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPrinter(curr_user,null));
        }

        async private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Printer item = (Printer)e.SelectedItem;

            await Navigation.PushAsync(new AddPrinter(curr_user, item));
        }
    }
}