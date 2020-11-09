using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using Xamarin.Forms;

namespace Login.Models
{
    public class Printer
    {
        [PrimaryKey, AutoIncrement]
        public int printer_id { get; set; }
        public string printer_name { get; set; }
        public float additional_cost { get; set; }
        public int category_id { get; set; }

        [DefaultValue(value: false)]
        public bool isColored { get; set; }
        [DefaultValue(value: false)]
        public bool availableToPurchase { get; set; }
        public string companyName { get; set; }

        public float get_feature_price()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            float sum = 0;

            List<PrinterFeature> features = db.Table<PrinterFeature>().Where(pf=>pf.printer_id==this.printer_id).ToList();

            foreach(PrinterFeature feature in features)
            {
                Feature temp = db.Table<Feature>().Where(f => f.feature_id == feature.feature_id).First();
                sum += temp.feature_price;
            }

            return sum;
        }

        public float get_total_price()
        {
            float sum = get_feature_price();

            sum += this.additional_cost;

            return sum;
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
