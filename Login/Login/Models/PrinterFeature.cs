using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Login.Models
{
    public class PrinterFeature
    {
        [PrimaryKey, AutoIncrement]
        public int printer_feature_id { get; set; }
        [NotNull]
        public int printer_id { get; set; }
        [NotNull]
        public int feature_id { get; set; }

        public override string ToString()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            Feature feature = db.Table<Feature>().Where(f=>f.feature_id==this.feature_id).First();
            return feature.feature_description;
        }
    }
}
