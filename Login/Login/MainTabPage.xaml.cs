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

            NavigationPage nav1 = new NavigationPage(new MainPage());
            nav1.Title = "User";

            NavigationPage nav2 = new NavigationPage(new UserViewPrinters());
            nav2.Title = "Printers";


            Children.Add(nav2);
            Children.Add(nav1);
        }
    }
}