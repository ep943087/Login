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
        User curr_user;
        public AddCategory(User c_user)
        {
            curr_user = c_user;
            InitializeComponent();
        }

        async private void Submit_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            db.CreateTable<Category>();

            Category newCategory = new Category()
            {
                category_name = category_name.Text
            };
            db.Insert(newCategory);
            db.Close();

            await DisplayAlert("New Category", category_name.Text + " was created", "OK");
            await Navigation.PopAsync();
        }
    }
}