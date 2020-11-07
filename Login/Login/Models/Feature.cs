using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Login.Models
{
    public class Feature
    {
        [PrimaryKey,AutoIncrement]
        public int feature_id { get; set; }
        public string feature_description { get; set; }
        public float feature_price { get; set; }

        public override string ToString()
        {
            return feature_description + "----" + feature_price;
        }
    }
}
