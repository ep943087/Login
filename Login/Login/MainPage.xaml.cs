using Login.Models;
using Login.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Login
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            checkAdmin();
        }

        private void create_printers()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            bool newdb = db.Table<Category>().Where(c => c.category_name == "Inkjet" || c.category_name == "Dot Matrix" || c.category_name == "Laser"||c.category_name=="All-In-One").FirstOrDefault() == null;

            if (newdb)
            {
                string[] categories = new string[] {"Inkjet","Dot Matrix","Laser","All-In-One" };
                Category temp;

                for(int i = 0; i < categories.Length; i++)
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
                    category_id = db.Table<Category>().Where(c=>c.category_name=="Inkjet").First().category_id,
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
            }
        }
        private void checkAdmin()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            // delete everything
            /*
            db.DeleteAll<User>();
            db.DeleteAll<Printer>();
            db.DeleteAll<Category>();
            db.DeleteAll<CartItem>();
            db.DeleteAll<Orders>();
            */

            // create all tables in main page
            db.CreateTable<User>();
            db.CreateTable<Printer>();
            db.CreateTable<Category>();
            db.CreateTable<CartItem>();
            db.CreateTable<Orders>();

            create_printers();

            User admin = db.Table<User>().Where(c => c.isAdmin).FirstOrDefault();

            if(admin == null)
            {
                admin = db.Table<User>().Where(c => c.username=="eliasproctor").FirstOrDefault();
                if(admin == null) 
                {
                    admin = new User
                    {
                        username = "eliasproctor",
                        password = "12345",
                        user_address = "1002 NW Gore Blvd",
                        user_card_num = "1234567890123456",
                        securyity_num = "123",
                        isAdmin = true
                    };
                    db.Insert(admin);
                }
                else
                {
                    admin.isAdmin = true;
                    db.Update(admin);
                }

            }
            db.Close();
        }
        async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {

            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            User searchUser = db.Table<User>().Where((c)=>
                c.username == username.Text && c.password == password.Text
            ).FirstOrDefault();

            if (searchUser == null)
            {
                await DisplayAlert("ERROR", "Username or password is incorrect", "Try, Again");
            }
            else
            {
                username.Text = password.Text = "";
                await DisplayAlert("SUCCESS", "YOU LOGGED IN SUCCESSFULLY", "OK");
                await Navigation.PushAsync(new Views.UserHomePage(searchUser));
            }
            db.Close();
        }
    }
}
