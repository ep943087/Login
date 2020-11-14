using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowAllCategories : ContentPage
    {
        public ShowAllCategories()
        {
            InitializeComponent();
        }

        private void update_list()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            db.CreateTable<Category>();
            listView.ItemsSource = db.Table<Category>().OrderBy(c => c.category_name).ToList();
            db.Close();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            update_list();
        }

        async private void add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCategory());

        }

        async private void MenuItem_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            Category cat = (Category)(listView.SelectedItem);
            Printer printer = null;
            printer = db.Table<Printer>().Where(p => p.category_id == cat.category_id).FirstOrDefault();

            if(printer == null)
            {
                await DisplayAlert("SUCCESS","Was deleted", "OK");
                /*
                db.Table<Category>().Delete(c=>c.category_id == cat.category_id);
                update_list();
                */
            }
            else
            {
                await DisplayAlert("At least one printer uses this category", "Could not delete category", "OK");
            }
        }
    }
}
