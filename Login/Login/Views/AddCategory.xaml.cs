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
    public partial class AddCategory : ContentPage
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        async private void Submit_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(category_name.Text))
            {
                await DisplayAlert("INVALID INPUT", "Category name cannot be empty", "OK");
                return;
            }

            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            db.CreateTable<Category>();

            Category newCategory = new Category()
            {
                category_name = category_name.Text
            };
            db.Insert(newCategory);
            db.Close();

            App.change = true;
            await DisplayAlert("New Category", category_name.Text + " was created", "OK");
            await Navigation.PopAsync();
        }
    }
}