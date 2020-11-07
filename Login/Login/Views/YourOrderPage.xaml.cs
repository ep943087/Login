using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Login.Models;

namespace Login.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourOrderPage : ContentPage
    {
        User curr_user;
        public YourOrderPage(User c_user)
        {
            curr_user = c_user;
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            orders.ItemsSource = Orders.get_by_user(curr_user);

        }

        async private void orders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Orders ord = (Orders)orders.SelectedItem;
            await Navigation.PushAsync(new OrderInfo(ord));
        }
    }
}