using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using Xamarin.Forms;

namespace Login.Models
{
    public class PrinterListItem : Printer
    {
        public string image { get; set; }
        public PrinterListItem(Printer p)
        {
            printer_id = p.printer_id;
            printer_name = p.printer_name;
            printer_price = p.printer_price;
            features = p.features;
            category_id = p.category_id;
            isColored = p.isColored;
            availableToPurchase = p.availableToPurchase;
            company_name = p.company_name;
            set_image();
        }

        private void set_image()
        {
            string name = company_name.ToLower();
            if (name == "hp")
                image = "https://images-na.ssl-images-amazon.com/images/I/71SdP6mbuZL._AC_SX355_.jpg";
            else if (name == "epson")
                image = "https://images-na.ssl-images-amazon.com/images/I/71pi8cVocbL._AC_SL1500_.jpg";
            else if (name == "canon")
                image = "https://pisces.bbystatic.com/image2/BestBuy_US/images/products/6042/6042704_sd.jpg";
            else if (name == "brother")
                image = "https://pisces.bbystatic.com/image2/BestBuy_US/images/products/6130/6130042_sd.jpg";
            else
                image = "https://previews.123rf.com/images/destinacigdem/destinacigdem1409/destinacigdem140900078/31538730-generic-computer-photo-printer-isolated-on-white-.jpg";
        }
    }
    public class Printer
    {
        [PrimaryKey, AutoIncrement]
        public int printer_id { get; set; }
        public string printer_name { get; set; }
        public float printer_price { get; set; }
        public string features { get; set; }
        public int category_id { get; set; }
        [DefaultValue(value: false)]
        public bool isColored { get; set; }
        [DefaultValue(value: false)]
        public bool availableToPurchase { get; set; }
        public string company_name { get; set; }

        public CartItem get_cart_item()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            return db.Table<CartItem>().Where(c => c.printer_id == this.printer_id && c.order_id == -1).First();
        }
        public float get_total_price()
        {
            return printer_price;
        }

        public string get_category_name()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            Category cat = db.Table<Category>().Where(c => c.category_id == this.category_id).FirstOrDefault();
            return cat == null ? "No Category" : cat.category_name;
        }

        public bool in_cart(User user)
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            CartItem item = db.Table<CartItem>().Where(c => c.order_id == -1 && c.printer_id == this.printer_id && c.user_id == user.user_id).FirstOrDefault();
            db.Close();
            return item != null;
        }

        public override string ToString()
        {
            return printer_name;
        }
    }
}
