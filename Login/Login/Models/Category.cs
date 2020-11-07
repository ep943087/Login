using System;
using System.Collections.Generic;
using System.Text;
using Login.Views;
using SQLite;
namespace Login.Models
{
    class Category
    {  
        [PrimaryKey,AutoIncrement]
        public int category_id { set; get; }
        public string category_name { set; get; }

        public override string ToString()
        {
            return category_name;
        }
    }
}
