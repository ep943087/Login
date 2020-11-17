using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Login.Models;
using SQLite;

namespace Login
{
    public partial class App : Application
    {
        public static string _dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "elias_proctor_printer_db.db3");
        public static User curr_user = null;
        public static bool change = true;
        public App()
        {

            create_printers();

            InitializeComponent();
            MainPage = new MainTabPage();
        }

        private void delete_all_records()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            db.DeleteAll<User>();
            db.DeleteAll<Printer>();
            db.DeleteAll<Category>();
            db.DeleteAll<CartItem>();
            db.DeleteAll<Orders>();

            db.Close();
        }

        private void create_printers()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            // create all tables in main page
            db.CreateTable<User>();
            db.CreateTable<Printer>();
            db.CreateTable<Category>();
            db.CreateTable<CartItem>();
            db.CreateTable<Orders>();

            bool newdb = db.Table<Category>().Where(c => c.category_name == "Inkjet" || c.category_name == "Dot Matrix" || c.category_name == "Laser" || c.category_name == "All-In-One").FirstOrDefault() == null;

            if (newdb)
            {
                string[] categories = new string[] { "Inkjet", "Dot Matrix", "Laser", "All-In-One" };
                Category temp;

                for (int i = 0; i < categories.Length; i++)
                {
                    temp = new Category
                    {
                        category_name = categories[i]
                    };
                    db.Insert(temp);
                }

                Printer p1 = new Printer
                {
                    printer_name = "Good Printer",
                    printer_price = 200f,
                    category_id = db.Table<Category>().Where(c => c.category_name == "Inkjet").First().category_id,
                    company_name = "Epson",
                    features = "Colored\nPrints Fast",
                    availableToPurchase = true
                };
                Printer p2 = new Printer
                {
                    printer_name = "Very Nice",
                    printer_price = 300f,
                    category_id = db.Table<Category>().Where(c => c.category_name == "All-In-One").First().category_id,
                    company_name = "HP",
                    features = "Black and White\nFax\nCopier\nScanner",
                    availableToPurchase = true
                };
                Printer p3 = new Printer
                {
                    printer_name = "Blazer",
                    printer_price = 250f,
                    category_id = db.Table<Category>().Where(c => c.category_name == "Laser").First().category_id,
                    company_name = "Canon",
                    features = "Colored\nHigh Speed",
                    availableToPurchase = true
                };
                Printer p4 = new Printer
                {
                    printer_name = "Plain",
                    printer_price = 150f,
                    category_id = db.Table<Category>().Where(c => c.category_name == "Dot Matrix").First().category_id,
                    company_name = "Brother",
                    features = "Black and White\nHigh Speed",
                    availableToPurchase = true
                };

                db.Insert(p1);
                db.Insert(p2);
                db.Insert(p3);
                db.Insert(p4);

                db.Close();
            }
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
