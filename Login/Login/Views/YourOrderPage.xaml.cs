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
        public YourOrderPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            orders.ItemsSource = Orders.get_by_user_list(App.curr_user);
        }

        async private void orders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Orders ord = (Orders)orders.SelectedItem;
            await Navigation.PushAsync(new OrderInfo(ord));
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}