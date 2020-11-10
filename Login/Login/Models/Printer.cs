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
                image = "https://store.hp.com/app/assets/images/product/1G5L3A%23B1H/center_facing.png?_=1604917565933&imwidth=430&imdensity=1";
            else if (name == "epson")
                image = "https://www.epson.eu/files/assets/converted/1500m-1500m/2/8/8/0/28801-productpicture-hires-et-15000_l14150.png.png";
            else
                image = "https://joelsenders.files.wordpress.com/2019/08/0014172_pcl-laser-printer-for-superpave-gyratory-compactor_600.jpeg";
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
