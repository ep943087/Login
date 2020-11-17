using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Login.Views;
namespace Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabPage : TabbedPage
    {
        public MainTabPage()
        {
            InitializeComponent();

            this.BarBackgroundColor = Color.Gray;
            this.SelectedTabColor = Color.White;
            this.UnselectedTabColor = Color.Black;

            NavigationPage nav1 = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Gray,
                BarTextColor = Color.White,
                Title = "Users",
            };

            NavigationPage nav2 = new NavigationPage(new UserViewPrinters())
            {
                BarBackgroundColor = Color.Gray,
                BarTextColor = Color.White,
                Title = "Printers"
            };


            Children.Add(nav2);
            Children.Add(nav1);
        }
    }
}