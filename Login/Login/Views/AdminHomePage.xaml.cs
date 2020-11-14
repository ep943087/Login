using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHomePage : ContentPage
    {
        public AdminHomePage()
        {
            InitializeComponent();
        }

        async private void users_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowAllUsers());
        }

        async private void categories_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowAllCategories());
        }

        async private void prints_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowAllPrintersAdminPage());
        }

        async private void orders_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowAllOrders());
        }
    }
}