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
            listView.ItemsSource = Category.get_all_categories_list_items();
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
            MenuItem item = (MenuItem)sender;
            Category cat = (Category)(item.CommandParameter);
            Printer printer = null;
            printer = db.Table<Printer>().Where(p => p.category_id == cat.category_id).FirstOrDefault();

            if(printer == null)
            {
                var answer = await DisplayAlert("Are you sure?","Deleting this category cannot be undone.","Yes","No");

                if (answer)
                {
                    await DisplayAlert("SUCCESS", "Was deleted", "OK");
                    db.Table<Category>().Delete(c => c.category_id == cat.category_id);
                    update_list();
                    App.change = true;
                }
            }
            else
            {
                await DisplayAlert( "Could not delete category", "At least one printer uses this category", "OK");
            }
        }
    }
}
