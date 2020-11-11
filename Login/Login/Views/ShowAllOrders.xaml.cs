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
    public partial class ShowAllOrders : ContentPage
    {
        User curr_user;
        public ShowAllOrders(User c_user)
        {
            curr_user = c_user;
            InitializeComponent();

            all_orders.ItemsSource = Orders.get_all_orders_list();
        }

        async private void all_orders_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Orders order = (Orders)e.SelectedItem;
            await Navigation.PushAsync(new OrderInfo(order));
        }
    }
}