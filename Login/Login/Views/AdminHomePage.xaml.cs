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
        private User curr_user;
        public AdminHomePage(User c_user)
        {
            curr_user = c_user;
            InitializeComponent();
        }

        async private void users_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowAllUsers(curr_user));
        }

        async private void categories_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowAllCategories(curr_user));
        }

        async private void prints_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowAllPrintersAdminPage(curr_user));
        }

        async private void orders_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowAllOrders(curr_user));
        }
    }
}