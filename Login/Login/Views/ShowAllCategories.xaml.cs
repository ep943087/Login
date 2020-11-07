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
        User curr_user;
        public ShowAllCategories(User c_user)
        {
            curr_user = c_user;
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            db.CreateTable<Category>();
            listView.ItemsSource = db.Table<Category>().OrderBy(c=>c.category_name).ToList();
            db.Close();
        }

        async private void add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCategory(curr_user));

        }
    }
}
