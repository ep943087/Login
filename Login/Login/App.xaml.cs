using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Login.Models;

namespace Login
{
    public partial class App : Application
    {
        public static string _dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDb.db3");
        public static User curr_user = null;
        public App()
        {
            System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDb.db3");

            InitializeComponent();

            MainPage = new MainTabPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
